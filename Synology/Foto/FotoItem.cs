// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Synology.Foto;

public class FotoItem : JsonModel {
	/// <summary> Name of file </summary>
	public required string filename { get; init; }

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

	public class List : JsonModel {
		/// <summary> List of items </summary>
		public required FotoItem[]? list { get; init; }
	}

	public class Additional : JsonModel {
		/// <summary>   </summary>
		public int? orientation { get; init; }

		/// <summary>   </summary>
		public int? orientation_original { get; init; }

		/// <summary>   </summary>
		public Resolution? resolution { get; init; }

		/// <summary>   </summary>
		public Thumbnail? thumbnail { get; init; }

		/// <summary>   </summary>
		public videoConvert[]? video_convert { get; init; }

		/// <summary>   </summary>
		public Dictionary<string, object?>? video_meta { get; init; }

		/// <summary>   </summary>
		public string? provider_user_id { get; init; }

		/// <summary>   </summary>
		public Tag[]? tag { get; init; }

		/// <summary>   </summary>
		public Dictionary<string, object?>? exif { get; init; }
	}

	public class videoConvert : JsonModel {
		public required string quality { get; init; }
		public videoConvertmetadata? metadata { get; init; }
	}

	public class videoConvertmetadata : JsonModel {
		public int duration { get; init; }                   //": 98000,
		public int orientation { get; init; }                //": 1,
		public int frame_bitrate { get; init; }              //": 896706,
		public int video_bitrate { get; init; }              //\": 763877,
		public int audio_bitrate { get; init; }              //\": 128290,
		public double framerate { get; init; }               //\": 23.976024627685548,
		public int resolution_x { get; init; }               //\": 640,
		public int resolution_y { get; init; }               //\": 360,
		public required string video_codec { get; init; }    //\": "h264",
		public required string audio_codec { get; init; }    //\": "aac_lc",
		public required string container_type { get; init; } //\": "mp4",
		public int video_profile { get; init; }              //\": 1,
		public int video_level { get; init; }                //\": 30,
		public int audio_frequency { get; init; }            //\": 44100,
		public int audio_channel { get; init; }              //\": 2
	}

	public class Tag : JsonModel {
		public int id { get; init; }
		public string? name { get; init; }
	}

	public class Resolution : JsonModel {
		/// <summary>   </summary>
		public int height { get; init; }

		/// <summary>   </summary>
		public int width { get; init; }
	}

	public class Thumbnail : JsonModel {
		public string? cache_key { get; init; }
		public string? m { get; init; }
		public string? preview { get; init; }
		public string? sm { get; init; }
		public int unit_id { get; init; }
		public string? xl { get; init; }
	}
}
