// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

namespace Synology;

/// <summary>
///     API Exception
/// </summary>
public class ApiException : Exception {
	/// <summary>
	///     Constructor
	/// </summary>
	/// <param name="message"></param>
	public ApiException(string message) : base(message) { }

	/// <summary>
	///     Constructor
	/// </summary>
	/// <param name="message"></param>
	/// <param name="ex"></param>
	public ApiException(string message, Exception ex) : base(message, ex) { }
}
