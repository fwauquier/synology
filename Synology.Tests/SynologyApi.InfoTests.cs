// MIT License
// Copyright (c) 2023 Frédéric Wauquier
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

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
}
