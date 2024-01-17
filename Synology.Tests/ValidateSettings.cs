// MIT License
// Copyright (c) 2023 Frédéric Wauquier
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

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
