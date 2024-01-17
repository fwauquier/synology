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
using Synology.Info;

// ReSharper disable UnusedMember.Global

namespace Synology;

public sealed partial class SynologyApi {
	public Login? LoginInformation { get; private set; }

	/// <summary>
	///     Login to the server
	/// </summary>
	public bool IsLoggedIn => LoginInformation is not null;

	[MemberNotNull(nameof(LoginInformation))]
	private void EnsureLoggedIn() {
		if (LoginInformation is null) throw new($"Not logged in to {Server}");
	}

	private void EnsureLoggedOut() {
		if (IsLoggedIn) AuthLogOut().GetAwaiter().GetResult();
	}

	/// <summary>
	/// Logout
	/// </summary>
	/// <returns>No specific response. It returns with an empty "success" response if completed without error.</returns>
	public async Task<string> AuthLogOut() {
		EnsureLoggedIn();
		var apiUrl = $"{Server}/webapi/entry.cgi?api=SYNO.API.Auth&version=6&method=logout&_sid={LoginInformation.sid}";
		var result = await GetResultAsString(apiUrl).ConfigureAwait(false);
		LoginInformation = null;
		return result;
	}

	/// <summary>
	///     Login
	/// </summary>
	/// <param name="user">Login account name.</param>
	/// <param name="password">Login account password.</param>
	/// <param name="session">(Optional) Login session name for DSM Applications.</param>
	/// <param name="format">
	///     (Optional) Returned format of session ID.
	///     <ul>
	///         Following are the two possible options and the default value is cookie.
	///         <li> cookie: The login session ID will be set to "id" key in cookie ofHTTP/HTTPS header of response.</li>
	///         <li>sid: The login sid will only be returned as response JSONdata and "id" key will not be set in cookie.</li>
	///     </ul>
	/// </param>
	/// <param name="opt_code">(Optional) 2-factor authentication option with an OTPcode. If it’s enabled, the user requires a verificationcode to log into DSM sessions.</param>
	/// <param name="enable_syno_token">(Optional) Obtain the CSRF token, also known as SynoToken, for the subsequent request. If set no, the server willnot produce this token.</param>
	/// <param name="enable_device_token">Optional) Omit 2-factor authentication (OTP) with adevice id for the next login request.</param>
	/// <param name="device_name">(Optional) To identify which device can be omitted from2-factor authentication (OTP), pass this value will skipit.</param>
	/// <param name="device_id">(Optional) If 2-factor authentication (OTP) has omittedthe same enabled device id, pass this value to skip it.</param>
	/// <returns></returns>
	/// <exception cref="ApiException"></exception>
	public async Task<bool> AuthLogin(string user,
		string password,
		string? session = null,
		string? format = null,
		string? opt_code = null,
		string? enable_syno_token = null,
		string? enable_device_token = null,
		string? device_name = null,
		string? device_id = null) {
		var parameters = GetParameters(new()
		                               {
			                               {"account", user},
			                               {"passwd", password},
			                               {"session", session},
			                               {"format", format},
			                               {"opt_code", opt_code},
			                               {"enable_syno_token", enable_syno_token},
			                               {"enable_device_token", enable_device_token},
			                               {"device_name", device_name},
			                               {"device_id", device_id}
		                               });

		var apiUrl  = $"{Server}/webapi/entry.cgi?api=SYNO.API.Auth&version=6&method=login&{parameters}";
		var json    = await GetResultAsString(apiUrl).ConfigureAwait(false);
		var result1 = DeserializeResponse<Login>(json);
		if (result1.success) {
			LoginInformation = result1.data;
			return LoginInformation is not null;
		}
		var error = result1.error;
		throw GetAuthException(error, result1.Serialize());
	}

	/// <summary>
	///     Query Syno Token<br />
	///     This API should be queried via Javascript, and store the SynoToken in Javascript variables.<br />
	///     If youreload your web page, SynoToken should be queried again.
	/// </summary>
	/// <returns></returns>
	/// <exception cref="ApiException"></exception>
	public async Task<bool> AuthToken() {
		var apiUrl  = $"{Server}/webapi/entry.cgi?api=SYNO.API.Auth&version=6&method=token";
		var json    = await GetResultAsString(apiUrl).ConfigureAwait(false);
		var result1 = DeserializeResponse<Login>(json);
		if (result1.success) {
			LoginInformation = result1.data;
			return LoginInformation is not null;
		}
		var error = result1.error;
		throw GetAuthException(error, result1.Serialize());
	}

	private static ApiException GetAuthException(ApiResponse<Login?>.ApiError? error, string json) {
		if (error is null) return new($"Login failed. No error returned. Content:\r\n{json}");
		switch (error.code) {
			case 400: return new("Login failed. No such account or incorrect password.");
			case 401: return new("Login failed. Disabled account.");
			case 402: return new("Login failed. Denied permission.");
			case 403: return new("Login failed. 2-factor authentication code required.");
			case 404: return new("Login failed. Failed to authenticate 2-factor authentication code.");
			case 406: return new("Login failed. Enforce to authenticate with 2-factor authentication code.");
			case 407: return new("Login failed. Blocked IP source.");
			case 408: return new("Login failed. Expired password cannot change.");
			case 409: return new("Login failed. Expired password.");
			case 410: return new("Login failed. Password must be changed.");
			default:  return new($"Login failed. Content:\r\n{error.Serialize()}");
		}
	}
}
