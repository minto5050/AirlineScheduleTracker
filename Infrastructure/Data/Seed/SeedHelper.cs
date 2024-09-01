using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection;

namespace Infrastructure.Data.Seed;

public class SeedHelper<T> where T : class
{
	public ICollection<T> LoadCsv(string filePath)
	{
		var entities = new List<T>();
		using (var reader = new StreamReader(filePath))
		{
			var config = new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				Delimiter=",",
				HasHeaderRecord = true,
				BufferSize = 500000
			};
			using (var csv = new CsvReader(reader, config))
			{
				RegisterClassMap(csv.Context);
				entities = csv.GetRecords<T>().ToList();
			}
		}
		return entities;
	}
	private void RegisterClassMap(CsvContext csv)
	{
		var mapType = typeof(ClassMap<>).MakeGenericType(typeof(T));
		var classMapType = Assembly.GetExecutingAssembly().GetTypes()
			.FirstOrDefault(t => mapType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

		if (classMapType != null)
		{
			var instance = Activator.CreateInstance(classMapType);
			var methodInfo = typeof(CsvContext).GetMethod("RegisterClassMap", new[] { typeof(ClassMap<T>) });
			if (methodInfo != null)
			{
				methodInfo.Invoke(csv, new[] { instance });
			}
		}
	}

}
