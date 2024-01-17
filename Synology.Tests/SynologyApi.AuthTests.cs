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
public class SynologyApi_Auth_Tests {
	public TestContext TestContext { get; set; } = default!;

	[TestMethod]
	public void Credentials() {
		TestContext.WriteLine($"Server {Settings.Server}");
		TestContext.WriteLine($"User1:Name {Settings.User1.Name}");
		TestContext.WriteLine($"User1:Password {Settings.User1.Password}");
	}

	[TestMethod]
	public async Task AuthLogin() {
		using var api = await Settings.GetApi(TestContext, false);
		Assert.IsTrue(await api.AuthLogin(Settings.User1.Name, Settings.User1.Password));
		api.LoginInformation.Dump();
	}

	[TestMethod]
	public async Task AuthLogOut() {
		using var api    = await Settings.GetApi(TestContext, true);
		var       result = api.AuthLogOut();
		result.Dump();
	}
	[TestMethod]
	public async Task AuthToken() {
		using var api    = await Settings.GetApi(TestContext, true);
		var       result = api.AuthToken();
		result.Dump();
	}
	[TestMethod]
	public async Task DummyAuthLogin() {
			using var api = await Settings.GetApi(TestContext, false);
			Assert.IsFalse(await api.AuthLogin(Settings.User1.Name,"cgY%QypqR7&iZ0Ow"));
			api.LoginInformation.Dump();
	}
}
