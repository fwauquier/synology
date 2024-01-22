// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

namespace Synology.Auth;

/// <summary>
///     Login response
/// </summary>
public class Login : JsonModel {
	/// <summary>
	///     A.k.a. device id, to identify which device.
	/// </summary>
	public string? did { get; init; }

	/// <summary>
	///     Authorized session ID.
	/// </summary>
	/// <remarks>When the user log in with format=sid, cookie will not be set and each API request shouldprovide a request parameter _sid=&lt;sid&gt; along with other parameters.</remarks>
	public string? sid { get; init; }

	/// <summary>
	///     Irrelevant.
	/// </summary>
	public bool? is_portal_port { get; init; }

	/// <summary>
	///     This token avoids Cross-Site RequestForgery.
	/// </summary>
	/// <remarks>Each subsequent API request should provide arequest parameter SynoToken=&lt;synotoken&gt; along with other parameters.</remarks>
	public string? synotoken { get; init; }

	public string? account { get; init; }
	public string? device_id { get; init; }
	public string? ik_message { get; init; }
}
