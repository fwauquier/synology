// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

using Microsoft.Extensions.Logging;

namespace Synology;

[TestClass]
public class ValidateSettings {
	public TestContext TestContext { get; set; } = default!;

	[TestMethod]
	public void ValidateSecret() {
		Assert.IsFalse(string.IsNullOrWhiteSpace(Settings.Server));
		Assert.IsFalse(string.IsNullOrWhiteSpace(Settings.User1.Name));
		Assert.IsFalse(string.IsNullOrWhiteSpace(Settings.User1.Password));
		Assert.IsTrue(Settings.ImageBaseFolder.Exists);
		Assert.IsTrue(Settings.Ffmpeg.Exists);
		Assert.IsTrue(Settings.Exiftool.Exists);
	}

	[TestMethod]
	public void ValidateLogger() {
		var logger = Settings.GetLogger(TestContext);
		Assert.IsNotNull(logger);
		logger.LogTrace("LogTrace");
		logger.LogDebug("LogDebug");
		logger.LogInformation("LogInformation");
		logger.LogWarning("LogWarning");
		logger.LogError("LogError");
		logger.LogCritical("LogCritical");
	}

	[TestMethod]
	public void Credentials() {
		TestContext.WriteLine($"Server             {Settings.Server}");
		TestContext.WriteLine($"ImageBaseFolder    {Settings.ImageBaseFolder.FullName}");
		TestContext.WriteLine($"User1:Name         {Settings.User1.Name}");
		TestContext.WriteLine($"User1:Password     {Settings.User1.Password}");
		TestContext.WriteLine($"Ffmpeg             {Settings.Ffmpeg.FullName}");
		TestContext.WriteLine($"Exiftool           {Settings.Exiftool.FullName}");
	}
}
