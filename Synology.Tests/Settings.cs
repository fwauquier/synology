// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Synology;

public static class Settings {
	static Settings() {
		var builder = new ConfigurationBuilder().AddUserSecrets(typeof(Settings).Assembly, false, false);

		var configuration = builder.Build();

		Server = configuration["Server"]!;
		ImageBaseFolder = new(configuration["ImageBaseFolder"]!);
		Ffmpeg = new(configuration["ffmpeg"]!);
		Exiftool = new(configuration["exiftool"]!);
		User1 = new()
		        {
			        Name = configuration["User1:Name"]!,
			        Password = configuration["User1:Password"]!
		        };
	}

	public static FileInfo Exiftool { get; set; }

	public static FileInfo Ffmpeg { get; set; }

	public static DirectoryInfo ImageBaseFolder { get; set; }

	public static string Server { get; }
	public static User User1 { get; }

	public static ILogger GetLogger(TestContext context, string? name = null, LogLevel logLevel = LogLevel.Trace) {
		return new TestContextLogger(context, name ?? typeof(Settings).FullName!, logLevel);
	}

	public static async Task<SynologyApi> GetApi(TestContext context, bool login = true, LogLevel logLevel = LogLevel.Debug) {
		var server = new SynologyApi(Server);
		server.Logger = GetLogger(context, server.GetType().FullName, logLevel);
		if (login)

			// ReSharper disable once UseConfigureAwaitFalse
			await server.AuthLogin(User1.Name, User1.Password);
		return server;
	}

	/// <summary>
	///     A logger that writes messages in the debug output window only when a debugger is attached.
	/// </summary>
	internal sealed class TestContextLogger : ILogger {
		private readonly string m_Name;
		private readonly TestContext m_TestContext;

		/// <summary>
		///     Initializes a new instance of the <see cref="TestContextLogger" /> class.
		/// </summary>
		/// <param name="testContext"></param>
		/// <param name="name">The name of the logger.</param>
		/// <param name="logLevel"></param>
		public TestContextLogger(TestContext testContext, string name, LogLevel logLevel = LogLevel.Information) {
			LogLevel = logLevel;
			m_TestContext = testContext;
			m_Name = name;
		}

		public LogLevel LogLevel { get; }

		/// <inheritdoc />
		public IDisposable BeginScope<TState>(TState state)
			where TState : notnull {
			return NullScope.Singleton;
		}

		/// <inheritdoc />
		public bool IsEnabled(LogLevel logLevel) {
			// Everything is enabled unless the debugger is not attached
			return logLevel >= LogLevel && logLevel != LogLevel.None;
		}

		/// <inheritdoc />
		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) {
			if (!IsEnabled(logLevel)) return;

			if (formatter is null) throw new NullReferenceException(nameof(formatter));

			var message = formatter(state, exception);

			if (string.IsNullOrEmpty(message)) return;

			message = $"{logLevel}: {message}";

			if (exception != null) message += Environment.NewLine + Environment.NewLine + exception;

			//m_TestContext.WriteLine("[" + _name + "]" + message);
			m_TestContext.WriteLine(message);
		}

		internal sealed class NullScope : IDisposable {
			private NullScope() { }

			public static NullScope Singleton { get; } = new();

			/// <inheritdoc />
			public void Dispose() { }
		}
	}

	public sealed class User {
		public string Name { get; init; } = default!;
		public string Password { get; init; } = default!;
	}
}
