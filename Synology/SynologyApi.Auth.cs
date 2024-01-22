// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Synology.Auth;

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
	///     Logout
	/// </summary>
	/// <returns>No specific response. It returns with an empty "success" response if completed without error.</returns>
	public async Task<string> AuthLogOut() {
		EnsureLoggedIn();
		Logger?.LogDebug("SYNO.API.Auth.logout");

		//var apiUrl = $"{Server}/webapi/entry.cgi?api=SYNO.API.Auth&version=6&method=logout&_sid={LoginInformation.sid}";
		var result = await GetResultAsString(ApiName.SYNO_API_Auth, "logout").ConfigureAwait(false);
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
		Logger?.LogDebug("AuthLogin");

		var json = await GetResultAsString(ApiName.SYNO_API_Auth,
		                                   "login",
		                                   new[]
		                                   {
			                                   UrlParameter.Get("account", user),
			                                   UrlParameter.Get("passwd", password),
			                                   UrlParameter.Get("session", session),
			                                   UrlParameter.Get("format", format),
			                                   UrlParameter.Get("opt_code", opt_code),
			                                   UrlParameter.Get("enable_syno_token", enable_syno_token),
			                                   UrlParameter.Get("enable_device_token", enable_device_token),
			                                   UrlParameter.Get("device_name", device_name),
			                                   UrlParameter.Get("device_id", device_id)
		                                   })
			.ConfigureAwait(false);
		var login = DeserializeResponse<Login>(json);
		if (login.success) {
			LoginInformation = login.data;
			return LoginInformation is not null;
		}

		var exception = GetAuthException(login.error, login.Serialize());
		Logger?.LogWarning("{Message}", exception.Message);
		throw exception;
	}

	/// <summary>
	///     Query Syno Token<br />
	///     This API should be queried via Javascript, and store the SynoToken in Javascript variables.<br />
	///     If youreload your web page, SynoToken should be queried again.
	/// </summary>
	/// <returns></returns>
	/// <exception cref="ApiException"></exception>
	public async Task<bool> AuthToken() {
		Logger?.LogDebug("AuthToken");
		var json  = await GetResultAsString(ApiName.SYNO_API_Auth, "token").ConfigureAwait(false);
		var login = DeserializeResponse<Login>(json);
		if (login.success) {
			LoginInformation = login.data;
			return LoginInformation is not null;
		}
		var exception = GetAuthException(login.error, login.Serialize());
		Logger?.LogWarning("{Message}", exception.Message);
		throw exception;
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
