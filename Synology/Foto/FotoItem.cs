// MIT License
// Copyright (c) 2023 Frédéric Wauquier
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Synology.Foto;

public class FotoItem {
	/// <summary> Name of file </summary>
	public string? filename { get; init; }

	/// <summary> Size of file </summary>
	public int? filesize { get; init; }

	/// <summary> ID  of folder </summary>
	public int? folder_id { get; init; }

	/// <summary>  ID of                     file </summary>
	public int? id { get; init; }

	/// <summary>   Linux timestamp in second Timestamp of indexing the file </summary>
	public long? indexed_time { get; init; }

	/// <summary> i.e.: photo (in case of iPhone live photos) </summary>
	public string? live_type { get; init; }

	/// <summary>UID      of owner  </summary>
	public int? owner_user_id { get; init; }

	/// <summary> Linux            timestamp in second Timestamp of file creation </summary>
	public long? time { get; init; }

	/// <summary>
	///     <ul>Type of data<li>photo: Photo</li><li>video: Video</li><li>live: iPhone live photos</li></ul>
	/// </summary>
	public string? type { get; init; }

	/// <summary> Additional informations </summary>
	public Additional? additional { get; init; }

#region Additional properties
	/// <summary>
	///     Additional properties
	/// </summary>
	[JsonExtensionData]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public Dictionary<string, object> AdditionalProperties { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#endregion

	public class List {
		/// <summary> List of items </summary>
		public List<FotoItem>? list { get; init; }

#region Additional properties
		/// <summary>
		///     Additional properties
		/// </summary>
		[JsonExtensionData]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Dictionary<string, object> AdditionalProperties { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#endregion
	}

	public class Additional {
		/// <summary>   </summary>
		public int? orientation { get; init; }

		/// <summary>   </summary>
		public int? orientation_original { get; init; }

		/// <summary>   </summary>
		public Resolution? resolution { get; init; }

		/// <summary>   </summary>
		public Thumbnail? thumbnail { get; init; }

		/// <summary>   </summary>
		public string? video_convert { get; init; }

		/// <summary>   </summary>
		public string? video_meta { get; init; }

		/// <summary>   </summary>
		public string? provider_user_id { get; init; }

		/// <summary>   </summary>
		public Tag[]? tag { get; init; }

		/// <summary>   </summary>
		public Dictionary<string, string>? exif { get; init; }

		// /// <summary>   </summary>
		// public string? aaaa { get; init; }
		// /// <summary>   </summary>
		// public string? aaaa { get; init; }
		// /// <summary>   </summary>
		// public string? aaaa { get; init; }
		// /// <summary>   </summary>
		// public string? aaaa { get; init; }
		// /// <summary>   </summary>
		// public string? aaaa { get; init; }
		// /// <summary>   </summary>
		// public string? aaaa { get; init; }
		// /// <summary>   </summary>
		// public string? aaaa { get; init; }
		// /// <summary>   </summary>
		// public string? aaaa { get; init; }
		// /// <summary>   </summary>
		// public string? aaaa { get; init; }
		// /// <summary>   </summary>
		// public string? aaaa { get; init; }
		// /// <summary>   </summary>
		// public string? aaaa { get; init; }

#region Additional properties
		/// <summary>
		///     Additional properties
		/// </summary>
		[JsonExtensionData]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Dictionary<string, object> AdditionalProperties { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#endregion
	}

	public class Tag {
		public int id { get; init; }
		public string? name { get; init; }
	}

	public class Resolution {
		/// <summary>   </summary>
		public int height { get; init; }

		/// <summary>   </summary>
		public int width { get; init; }

#region Additional properties
		/// <summary>
		///     Additional properties
		/// </summary>
		[JsonExtensionData]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Dictionary<string, object> AdditionalProperties { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#endregion
	}

	public class Thumbnail {
		public string? cache_key { get; init; }
		public string? m { get; init; }
		public string? preview { get; init; }
		public string? sm { get; init; }
		public int unit_id { get; init; }
		public string? xl { get; init; }

#region Additional properties
		/// <summary>
		///     Additional properties
		/// </summary>
		[JsonExtensionData]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Dictionary<string, object> AdditionalProperties { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#endregion
	}
}
