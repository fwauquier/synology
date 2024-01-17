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

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.Extensions.Logging;

// ReSharper disable UnusedMember.Global

namespace Synology;

/// <summary>
/// Synology API Wrapper
/// </summary>
public sealed partial class SynologyApi : IDisposable {
	private readonly HttpClient m_HttpClient;

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="server"></param>
	public SynologyApi(string server) {
		Server = server;

		m_HttpClient = new();
	}

	/// <summary>
	/// Logger
	/// </summary>
	public ILogger? Logger { get; set; }

	/// <summary>
	/// Server
	/// </summary>
	public string Server { get; }

	/// <inheritdoc />
	public void Dispose() {
		Logger?.LogInformation("[Logout and client disposing] {Server}", Server);
		EnsureLoggedOut();
		m_HttpClient.Dispose();
	}

	/// <summary>
	/// Send a POST request to the server and return the result as a string
	/// </summary>
	/// <param name="requestUri"></param>
	/// <param name="content"></param>
	/// <returns></returns>
	public async Task<string> PostResultAsString([StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, HttpContent? content = null) {
		Logger?.LogDebug("[POST] {RequestUri}", requestUri);
		var response = await m_HttpClient.PostAsync(requestUri, content).ConfigureAwait(false);

		// Check if the request was successful
		if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
		return new HttpRequestException($"Request failed with status code {response.StatusCode}").Message;

	}

	public async Task<string> GetResultAsString([StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri) {
		Logger?.LogDebug("[GET] {RequestUri}", requestUri);
		var response = await m_HttpClient.GetAsync(requestUri).ConfigureAwait(false);

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


	public T? GetAndValidate<T>(string json) {
		Logger?.LogTrace("[Deserialize]Source {Json}", json);
		var response = JsonSerializer.Deserialize<ApiResponse<T?>>(json);
		if (response is null) throw new ApiException($"Cannot deserialize string value to ApiResponse<{typeof(T).Name}>. Content:\r\n{json}");
		if (response.success) {
			#if DEBUG
			if( response.data is JsonModel jsonModel)
				jsonModel.EnsureAllDataDeserialized();
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
		var obj = JsonSerializer.Deserialize<ApiResponse<T?>>(json);
		if (obj is null) throw new ApiException($"Cannot deserialize string value to ApiResponse<{typeof(T).Name}>. Content:\r\n{json}");
		#if DEBUG
		if(obj.data is JsonModel jsonModel)
			jsonModel.EnsureAllDataDeserialized();
		#endif
		return obj;
	}

	// ReSharper disable UnusedAutoPropertyAccessor.Local
	private sealed class ApiResponse<T> {
		public T? data { get; init; }
		public required bool success { get; init; }
		public ApiError? error { get; init; }

		public sealed class ApiError {
			public required int code { get; set; }

			// ReSharper disable once UnusedMember.Local
			public required Dictionary<string, string>? errors { get; set; }
		}
	}
	// ReSharper restore UnusedAutoPropertyAccessor.Local

	private static string GetParameters(Dictionary<string, string?> dictionary) {
		var items = new List<string>();
		foreach(var item in dictionary)
			if (item.Value is not null)
				items.Add($"{item.Key}={Uri.EscapeDataString(item.Value)}");
		return string.Join("&", items);
	}
}
