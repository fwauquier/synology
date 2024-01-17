// MIT License
// Copyright (c) 2023 Frédéric Wauquier
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Synology.Info;

namespace Synology;

[TestClass]
public class SynologyAPI_Info_Tests {
	public TestContext TestContext { get; set; } = default!;

	[DataTestMethod]
	[DataRow("all")]
	[DataRow("SYNO.Foto.")]
	[DataRow("SYNO.Foto.Browse.")]
	[DataRow("SYNO.Foto.Browse.Item")]
	[DataRow("SYNO.Foto.Browse.Folder")]
	[DataRow("SYNO.FotoTeam.")]
	[DataRow("SYNO.API.")]
	[DataRow("SYNO.API.Auth")]
	[DataRow("SYNO.API.Info")]
	[DataRow("SYNO.FileStatio")]
	public async Task GetApiInfoMethods(string query) {
		using var api  = await Settings.GetApi(TestContext, false);
		var       data = await api.GetApiInfoMethods(query);
			data.Dump();
	}

	public static bool IsImplemented(string name) {
		return name.StartsWith("SYNO.Foto", StringComparison.OrdinalIgnoreCase) || name.StartsWith("SYNO.API.Auth", StringComparison.OrdinalIgnoreCase) || name.StartsWith("SYNO.API.Info", StringComparison.OrdinalIgnoreCase)
			;
	}

	[TestMethod]
	public async Task GenerateApiNameEnum( ) {
		using var api  = await Settings.GetApi(TestContext, false);
		var       data = await api.GetApiInfoMethods("all");
		Assert.IsNotNull(data);
		data=data.Where(i=>IsImplemented(i.Key)).ToDictionary(i=>i.Key,i=>i.Value);
		TestContext.WriteLine("internal enum ApiName {");
		foreach(var item in data.OrderBy(i=>i.Key)) {
			TestContext.WriteLine($"	/// <summary>\tAPI:{item.Key} path:{item.Value.path} version:{item.Value.minVersion} to {item.Value.maxVersion}</summary>");
			TestContext.WriteLine($"	{GetEnumName(item)},");
		}
		TestContext.WriteLine("}");

	}
		[TestMethod]
	public async Task GenerateApiNameEnumExtensions( ) {
		using var api  = await Settings.GetApi(TestContext, false);
		var       data = await api.GetApiInfoMethods("all");
		Assert.IsNotNull(data);
		data=data.Where(i=>IsImplemented(i.Key)).ToDictionary(i=>i.Key,i=>i.Value);

		TestContext.WriteLine("internal static class ApiNameExtensions{");
    		GenerateHelper("GetApiName","string",
		               data.Select(i=>new KeyValue("ApiName."+GetEnumName(i ),$"\"{i.Key}\"")).ToArray());

		GenerateHelper("GetPath","string",
		               data.Select(i=>new KeyValue("ApiName."+GetEnumName(i ),$"\"{i.Value.path}\"")).ToArray());
		GenerateHelper("GetRequestFormat","string",
		               data.Select(i=>new KeyValue("ApiName."+GetEnumName(i ),$"\"{i.Value.requestFormat}\"")).ToArray());
		GenerateHelper("GetMaxVersion","int",
		               data.Select(i=>new KeyValue("ApiName."+GetEnumName(i ),$"{i.Value.maxVersion}")).ToArray());
		GenerateHelper("GetMinVersion","int",
		               data.Select(i=>new KeyValue("ApiName."+GetEnumName(i ),$"{i.Value.minVersion}")).ToArray());



		TestContext.WriteLine("}");
	}

	private void GenerateHelper(string methodName,string returnType, KeyValue[] values) {
		TestContext.WriteLine($"public static {returnType} {methodName}(this ApiName value){{");
		TestContext.WriteLine("\tswitch(value){");

		// Create a dictionary with returned values as key and the list of enum values as value
		var groups = values.GroupBy(i => i.Value).ToDictionary(i=>i.Key,i=>i.Select(j=>j.Case).ToArray());

		var mostUsedValue = groups.MaxBy(i => i.Value.Length);

		foreach (var group in groups.OrderBy(i=>i.Key)) {
			if (group.Key == mostUsedValue.Key && group.Value.Length > 1) continue;
			if (group.Value.Length == 1) {
				TestContext.WriteLine($"\tcase {group.Value[0]}: return {group.Key};");
			} else {
				foreach (var item in group.Value)
					TestContext.WriteLine($"\tcase {item}:");
				TestContext.WriteLine($"\t\treturn {group.Key};");

			}
		}
		if(mostUsedValue.Value.Length>1)
			TestContext.WriteLine($"\tdefault: : return {mostUsedValue.Key};");
		else
			TestContext.WriteLine("\tdefault: throw new ArgumentOutOfRangeException(nameof(value), value, null);");
		TestContext.WriteLine("}");
		TestContext.WriteLine("}");
		TestContext.WriteLine("");
	}

	private sealed record KeyValue(string Case, string Value) {

	}

	private static string GetEnumName(KeyValuePair<string, ApiInfo> item)
	{
		return item.Key.Replace(".","_");
	}
}
