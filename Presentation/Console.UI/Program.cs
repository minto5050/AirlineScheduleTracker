using Application;
using CommandLine;
using ConsoleUI;
using ConsoleUI.Abstracts;
using ConsoleUI.Concretes;
using ConsoleUI.Models;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

public class Program
{
	public static async Task Main(string[] args)
	{
		Splash();
		var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("Config.json", optional: false, reloadOnChange: true)
				.Build();
		
		Parser.Default.ParseArguments<Options>(args)
				.WithParsed(async opts =>
				{

					var serviceProvider = new ServiceCollection()
					.AddSingleton<IBackboneService, BackboneService>()
					.AddAppLayer()
					.AddInfraLayer(configuration)
					.AddTransient<ConsoleUIApp>()
					.AddScoped<IOutputService,CsvOutputService>()
					.AddLogging(configuration =>
					{
						configuration.AddConsole();
						if (opts.Verbosity != "diag")
						{
							configuration.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.None);
							configuration.AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.None);
							configuration.AddFilter("Microsoft.EntityFrameworkCore.Query", LogLevel.None);
						}
						if(opts.Verbosity == "trace")
						{
							configuration.AddFilter("Application.Flights*", LogLevel.Trace);
						}
					})
					.BuildServiceProvider();
					var app = serviceProvider.GetService<ConsoleUIApp>();
					//await app.InitializeDb();
					await app.Run(opts);

				})
				.WithNotParsed<Options>((errs) => HandleParseError(errs));
	}

	private static void Splash()
	{
		PrintAsciiArt(@"./Assets/Splash3.txt");
		Thread.Sleep(500);
		PrintAsciiArt(@"./Assets/Splash2.txt");
		Thread.Sleep(500);
		PrintAsciiArt(@"./Assets/Splash1.txt");
		Thread.Sleep(300);
		Console.Clear();
		PrintAsciiArt(@"./Assets/name.txt");
	}

	private static void PrintAsciiArt(string filePath)
	{
		if (File.Exists(filePath))
		{
			string asciiArt = File.ReadAllText(filePath);
			Console.Clear();
			Console.WriteLine(asciiArt);
		}
	}

	private static object HandleParseError(IEnumerable<Error> errs)
	{
		var result = -2;
		Debug.WriteLine("errors {0}", errs.Count());
		if (errs.Any(x => x is HelpRequestedError || x is VersionRequestedError))
			result = -1;
		Console.WriteLine("Exit code {0}", result);
		return result;
	}
}