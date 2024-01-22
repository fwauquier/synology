// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

namespace Synology.Info;

/// <summary>
///     API description objects
/// </summary>
public class ApiInfo : JsonModel {
	/// <summary>
	///     API name
	/// </summary>
	public string? key { get; init; }

	/// <summary>
	///     Maximum supported API version
	/// </summary>
	public int maxVersion { get; init; }

	/// <summary>
	///     Minimum supported API version
	/// </summary>
	public int minVersion { get; init; }

	/// <summary>
	///     API path
	/// </summary>
	public required string path { get; init; }

	/// <summary>
	///     If this value shows "JSON", use the JSON encoder for allother parameter values.
	/// </summary>
	public string? requestFormat { get; init; }
}
