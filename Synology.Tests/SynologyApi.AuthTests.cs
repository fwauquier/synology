// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

using Microsoft.Extensions.Logging;

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
		using var api = await Settings.GetApi(TestContext, false, LogLevel.Trace);
		Assert.IsTrue(await api.AuthLogin(Settings.User1.Name, Settings.User1.Password));
		api.LoginInformation.Dump();
	}

	[TestMethod]
	public async Task AuthLogOut() {
		using var api    = await Settings.GetApi(TestContext);
		var       result = await api.AuthLogOut();
		result.Dump();
	}

	[TestMethod]
	public void AuthToken() {
		Assert.ThrowsException<ApiException>(() => {
			using var api    = Settings.GetApi(TestContext).GetAwaiter().GetResult();
			var       result = api.AuthToken().GetAwaiter().GetResult();
			result.Dump();
		});
	}

	[TestMethod]
	public void DummyAuthLogin() {
		Assert.ThrowsException<ApiException>(() => {
			using var api = Settings.GetApi(TestContext, false).GetAwaiter().GetResult();
			api.AuthLogin("3YI5!HskKTItaPVHZmEFUbY9Z3#PnrIpBDCgk!r6D", "cgY%QypqR7&iZ0Ow").GetAwaiter().GetResult();
		});
	}
}
