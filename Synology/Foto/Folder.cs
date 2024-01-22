// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Synology.Foto;

public sealed class Folder : JsonModel {
	/// <summary> ID of folder  </summary>
	public required int id { get; init; }

	/// <summary> Folder name  </summary>
	public required string name { get; init; }

	/// <summary>  UID of album owner </summary>
	public int owner_user_id { get; init; }

	/// <summary>  ID of parent directory </summary>
	public int parent { get; init; }

	/// <summary> Passphrase  </summary>
	public string? passphrase { get; init; }

	/// <summary>   </summary>
	public bool shared { get; init; }

	/// <summary>   </summary>
	public string? sort_by { get; init; }

	/// <summary>   </summary>
	public string? sort_direction { get; init; }

	// ReSharper disable once ClassNeverInstantiated.Global
	internal sealed class List : JsonModel {
		/// <summary> List of items </summary>
		public Folder[]? list { get; init; }
	}
}
