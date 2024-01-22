// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

namespace Synology;

public sealed class ApiResponse<T> {
	public T? data { get; init; }
	public required bool success { get; init; }
	public ApiError? error { get; init; }

	public sealed class ApiError {
		public required int code { get; set; }

		// ReSharper disable once UnusedMember.Local
		public Dictionary<string, string>? errors { get; set; }
	}
}
