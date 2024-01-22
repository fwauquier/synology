// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

// ReSharper disable UnusedMember.Global

namespace Synology;

/// <summary>
///     Synology API Wrapper
/// </summary>
public sealed partial class SynologyApi : IDisposable {
	private readonly HttpClient m_HttpClient;

	/// <summary>
	///     Constructor
	/// </summary>
	/// <param name="server"></param>
	public SynologyApi(string server) {
		Server = server.Trim('/');

		m_HttpClient = new();
	}

	/// <summary>
	///     Logger
	/// </summary>
	public ILogger? Logger { get; set; }

	/// <summary>
	///     Server
	/// </summary>
	public string Server { get; }

	/// <inheritdoc />
	public void Dispose() {
		Logger?.LogInformation("Dispose SynologyAPI object for server {Server}", Server);
		EnsureLoggedOut();
		m_HttpClient.Dispose();
	}

	/// <summary>
	///     Send a POST request to the server and return the result as a string
	/// </summary>
	/// <returns></returns>
	internal async Task<string> PostResultAsString(ApiName apiName, string method, List<UrlParameter>? parameters = null, HttpContent? content = null) {
		var uri = GetUri(apiName, method, parameters);
		Logger?.LogTrace("[POST] {RequestUri}", uri);
		var response = await m_HttpClient.PostAsync(uri, content).ConfigureAwait(false);

		// Check if the request was successful
		if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
		return new HttpRequestException($"Request failed with status code {response.StatusCode}").Message;
	}

	private Uri GetUri(ApiName apiName, string method, IReadOnlyCollection<UrlParameter>? parameters = null, bool addSid = true, bool addSynoToken = true) {
		//var apiUrl = $"{Server}/webapi/entry.cgi?api=SYNO.API.Auth&version=6&method=login&{parameters}";
		var sb = new StringBuilder(Server);
		sb.Append("/webapi/");
		sb.Append(apiName.GetPath());
		sb.Append("?api=");
		sb.Append(apiName.GetApiName());
		sb.Append("&version=");
		sb.Append(apiName.GetMaxVersion());
		sb.Append("&method=");
		sb.Append(method);
		if (parameters is not null && parameters.Count > 0)
			foreach (var item in parameters)
				item.AddTo(sb);
		if (LoginInformation is not null) {
			if (addSid && !string.IsNullOrWhiteSpace(LoginInformation.sid)) {
				sb.Append("&_sid=");
				sb.Append(LoginInformation.sid);
			}
			if (addSynoToken && !string.IsNullOrWhiteSpace(LoginInformation.synotoken)) {
				sb.Append("&SynoToken=");
				sb.Append(LoginInformation.synotoken);
			}
		}

		return new(sb.ToString(), UriKind.Absolute);
	}

	internal async Task<string> GetResultAsString(ApiName apiName, string method, IReadOnlyCollection<UrlParameter>? parameters = null) {
		var uri = GetUri(apiName, method, parameters);
		Logger?.LogTrace("[GET] {RequestUri}", uri);
		var response = await m_HttpClient.GetAsync(uri).ConfigureAwait(false);

		// Check if the request was successful
		if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
		return new HttpRequestException($"Request failed with status code {response.StatusCode}").Message;

		// // Read response content
		// var responseData = await response.Content.ReadAsStringAsync();
		//
		// // Process the responseData
		// Console.WriteLine(responseData);
		// Console.WriteLine($"Request failed with status code {response.StatusCode}");
	}

	internal T? GetAndValidate<T>(string jsonString) {
		Logger?.LogTrace("[Deserialize]Source {Json}", jsonString);
		ApiResponse<T?>? deserialize;
#if DEBUG
		jsonString = JsonModel.Reformat(jsonString);
		try {
			deserialize = JsonSerializer.Deserialize<ApiResponse<T?>>(jsonString);
		} catch (JsonException e) {
			var lines = jsonString.Split(Environment.NewLine);
			var iLine = e.LineNumber ?? int.MaxValue;
			var line  = lines.Length > iLine ? lines[iLine] : string.Empty;

			throw new ApiException($"Cannot deserialize response to {typeof(T).FullName}:{e.Message}"
			                       + $"{Environment.NewLine}Line: {iLine}"
			                       + $"{Environment.NewLine}BytePositionInLine: {e.BytePositionInLine}"
			                       + $"{Environment.NewLine}Path:{e.Path}"
			                       + $"{Environment.NewLine}Line content:{line}"
			                       + $"{Environment.NewLine}{jsonString}",
			                       e);
		}

#else
		deserialize = JsonSerializer.Deserialize<Response<T>>(jsonString);
#endif
		if (deserialize is null) throw new ApiException($"Cannot deserialize response to {typeof(T).FullName}");
		if (deserialize.success) {
#if DEBUG
			if (deserialize.data is JsonModel jsonModel) jsonModel.EnsureAllDataDeserialized();
#endif
			return deserialize.data;
		}
		if (deserialize.error is null) throw new ApiException($"Request failed. No error returned. Content:\r\n{deserialize.Serialize()}");
		throw new($"Request failed. {GetErrorDescription(deserialize.error.code)} Content:\r\n{deserialize.Serialize()}");

		static string GetErrorDescription(int code) {
			return code switch
			{
				100             => "Unknown error.",
				101             => "No parameter of API, method or version.",
				102             => "The requested API does not exist.",
				103             => "The requested method does not exist.",
				104             => "The requested version does not support the functionality.",
				105             => "The logged in session does not have permission.",
				106             => "Session timeout.",
				107             => "Session interrupted by duplicated login.",
				108             => "Failed to upload the file.",
				109             => "The network connection is unstable or the system is busy.",
				110             => "The network connection is unstable or the system is busy.",
				111             => "The network connection is unstable or the system is busy.",
				117             => "The network connection is unstable or the system is busy.",
				118             => "The network connection is unstable or the system is busy.",
				112             => "Preserve for other purpose.",
				115             => "Not allowed to upload a file.",
				116             => "Not allowed to perform for a demo site.",
				119             => "Invalid session.",
				113             => "Preserve for other purpose.",
				> 120 and < 150 => "Preserve for other purpose.",
				150             => "Request source IP does not match the login IP.",
				_               => "Unknown error."
			};
		}
	}

	private ApiResponse<T?> DeserializeResponse<T>(string json) {
		var              jsonString = json;
		ApiResponse<T?>? deserialize;
		Logger?.LogTrace("Deserialize: {JsonString}", jsonString);

#if DEBUG
		jsonString = JsonModel.Reformat(jsonString);
		try {
			deserialize = JsonSerializer.Deserialize<ApiResponse<T?>>(jsonString);
		} catch (JsonException e) {
			var lines = jsonString.Split(Environment.NewLine);
			var iLine = e.LineNumber ?? int.MaxValue;
			var line  = lines.Length > iLine ? lines[iLine] : string.Empty;

			throw new ApiException($"Cannot deserialize response to {typeof(T).FullName}:{e.Message}"
			                       + $"{Environment.NewLine}Line: {iLine}"
			                       + $"{Environment.NewLine}BytePositionInLine: {e.BytePositionInLine}"
			                       + $"{Environment.NewLine}Path:{e.Path}"
			                       + $"{Environment.NewLine}Line content:{line}"
			                       + $"{Environment.NewLine}{jsonString}",
			                       e);
		}

#else
		deserialize = JsonSerializer.Deserialize<Response<T>>(jsonString);
#endif

		if (deserialize is null) throw new ApiException($"Cannot deserialize response to {typeof(T).FullName}");
		if (deserialize.success == false) {
			var sb     = new StringBuilder();
			var errors = deserialize.error?.errors;
			if (errors != null)
				foreach (var pair in errors)
					sb.AppendLine($"{pair.Key}:{pair.Value}");
			throw new ApiException($"Response is not successfully. Code:{deserialize.error?.code}\r\n{sb}");
		}
#if DEBUG
		var result = deserialize.data;
		if (result is JsonModel jsonModel)
			jsonModel.EnsureAllDataDeserialized();
		else if (result is IEnumerable<JsonModel> jsonModels)
			foreach (var jsonModel2 in jsonModels)
				jsonModel2.EnsureAllDataDeserialized();
#endif
		return deserialize;
	}
}
