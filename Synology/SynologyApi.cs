// MIT License
// Copyright (c) 2023 Frédéric Wauquier
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software
// is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

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
	internal async Task<string> PostResultAsString(ApiName apiName,string method, Dictionary<string, string?>? parameters=null, HttpContent? content = null) {
		var uri=GetUri(apiName,method,parameters);
		Logger?.LogTrace("[POST] {RequestUri}", uri);
		var response = await m_HttpClient.PostAsync(uri, content).ConfigureAwait(false);

		// Check if the request was successful
		if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
		return new HttpRequestException($"Request failed with status code {response.StatusCode}").Message;
	}

	private   Uri GetUri(ApiName apiName,string method, Dictionary<string, string?>? parameters=null,bool addSid=true,bool addSynoToken=true) {
		//var apiUrl = $"{Server}/webapi/entry.cgi?api=SYNO.API.Auth&version=6&method=login&{parameters}";
		var sb = new StringBuilder( Server);
		sb .Append("/webapi/");
		sb .Append(apiName.GetPath());
		sb .Append("?api=");
		sb .Append(apiName.GetApiName());
		sb .Append("&version=");
		sb .Append(apiName.GetMaxVersion());
		sb .Append("&method=");
		sb .Append(method);
		if(parameters is not null && parameters.Count>0) {

			foreach (var item in parameters) {
				if (item.Value is null) continue;
				sb.Append('&');
				sb.Append(item.Key);
				sb.Append('=');
				sb.Append(Uri.EscapeDataString(item.Value));
			}
		}
		if (LoginInformation is not null) {
			if(addSid && !string.IsNullOrWhiteSpace( LoginInformation.sid)){
				sb.Append("&_sid=");
				sb.Append(LoginInformation.sid);
			}
			if(addSynoToken && !string.IsNullOrWhiteSpace( LoginInformation.synotoken)){
				sb.Append("&SynoToken=");
				sb.Append(LoginInformation.synotoken);
			}

		}
		return new Uri(sb.ToString(),UriKind.Absolute);



	}
	internal async Task<string> GetResultAsString(ApiName apiName,string method, Dictionary<string, string?>? parameters=null ) {
		var uri =GetUri(apiName,method,parameters);
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

	internal T? GetAndValidate<T>(string json) {
		Logger?.LogTrace("[Deserialize]Source {Json}", json);
		var response = JsonSerializer.Deserialize<ApiResponse<T?>>(json)
		               ?? throw new ApiException($"Cannot deserialize string value to ApiResponse<{typeof(T).Name}>. Content:\r\n{json}");
		if (response.success) {
#if DEBUG
			if (response.data is JsonModel jsonModel) jsonModel.EnsureAllDataDeserialized(json);
#endif
			return response.data;
		}
		if (response.error is null) throw new ApiException($"Request failed. No error returned. Content:\r\n{response.Serialize()}");
		throw new($"Request failed. {GetErrorDescription(response.error.code)} Content:\r\n{response.Serialize()}");

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
		Logger?.LogTrace("[Deserialize]Source {Json}", json);
		try {
			var obj = JsonSerializer.Deserialize<ApiResponse<T?>>(json);
			if (obj is null) throw new ApiException($"Cannot deserialize string value to ApiResponse<{typeof(T).Name}>.{Environment.NewLine}*** JSON Content ***************************\r\n{json}{Environment.NewLine}");
#if DEBUG
			if (obj.data is JsonModel jsonModel) jsonModel.EnsureAllDataDeserialized(json);
#endif
			return obj;
		} catch (Exception ex) {
			throw new ApiException($"Cannot deserialize string value to ApiResponse<{typeof(T).Name}>. Content:\r\n{json}", ex);
		}
	}


	// ReSharper disable UnusedAutoPropertyAccessor.Local
	private sealed class ApiResponse<T> {
		public T? data { get; init; }
		public required bool success { get; init; }
		public ApiError? error { get; init; }

		public sealed class ApiError {
			public required int code { get; set; }

			// ReSharper disable once UnusedMember.Local
			public   Dictionary<string, string>? errors { get; set; }
		}
	}

	// ReSharper restore UnusedAutoPropertyAccessor.Local
}
