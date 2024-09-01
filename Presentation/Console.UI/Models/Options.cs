using CommandLine;
using System.Globalization;

namespace ConsoleUI.Models
{
	public sealed class Options
	{
		[Option('a',"agencyid",Required = true,HelpText = "The Agency Id")]
        public int AgencyId { get; set; }

		[Option('s',"startdate",Required =true,HelpText = "start date (in yyyy-mm-dd format)")]
		public string StartDateString { get; set; }
		public DateTime StartDate { get => DateTime.ParseExact(StartDateString, "yyyy-mm-dd", CultureInfo.InvariantCulture); }

		[Option('e',"enddate",Required =true,HelpText = "end date (in yyyy-mm-dd format)")]
		public string EndDateString { get; set; }
		public DateTime EndDate { get => DateTime.ParseExact(EndDateString, "yyyy-mm-dd", CultureInfo.InvariantCulture); }

		[Option('v', "verbosity", Required = false, HelpText = "Program verbosity, supported options are trace,info,diag")]
		public string Verbosity { get; set; } = "silent";
		[Option('o', "output", Required = false, HelpText = "Output file path to write the results to")]
		public string OutputPath { get; set; } = $"./air-line-schedule-result_{DateTime.Now.Ticks}.csv";
    }
}
