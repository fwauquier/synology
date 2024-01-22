﻿// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Synology;

internal static class Helper {
	public static readonly JsonSerializerOptions jsonOptionIndented = new()
	                                                                  {
		                                                                  WriteIndented = true,
		                                                                  IgnoreReadOnlyFields = true,
		                                                                  IgnoreReadOnlyProperties = true,
		                                                                  DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
	                                                                  };

	public static readonly JsonSerializerOptions jsonOptionIndentedAll = new()
	                                                                     {
		                                                                     WriteIndented = true,
		                                                                     IgnoreReadOnlyFields = true,
		                                                                     IgnoreReadOnlyProperties = true,
		                                                                     DefaultIgnoreCondition = JsonIgnoreCondition.Never
	                                                                     };

	private static readonly Regex guidRegex = new(@"\b[A-F0-9]{8}(?:-[A-F0-9]{4}){3}-[A-F0-9]{12}\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

	private static readonly DateTime RefDate = new(2023, 1, 1);

	//public static readonly System.Text.Json.JsonSerializerOptions jsonOption = new System.Text.Json.JsonSerializerOptions() {WriteIndented = false};

	public static void Dump(this object? obj) {
		Console.WriteLine(Serialize(obj));
	}

	public static void DumpAll(this object? obj) {
		Console.WriteLine(JsonSerializer.Serialize(obj, jsonOptionIndentedAll));
	}

	public static async Task DumpAsync<T>(this Task<T?> obj) {
		Console.WriteLine(Serialize(await obj.ConfigureAwait(false)));
	}

	public static string Serialize(this object? obj) {
		try {
			return JsonSerializer.Serialize(obj, jsonOptionIndented);
		} catch {
			return JsonSerializer.Serialize(obj, jsonOptionIndented);
		}
	}

	public static void ExecuteInFolder(DirectoryInfo di) {
		Console.WriteLine($"* {di.FullName}");

		foreach (var item in di.GetDirectories()) ExecuteInFolder(item);

		foreach (var file in di.GetFiles("*.*")) {
			var extension     = file.Extension.ToLowerInvariant();
			var fileName      = Path.GetFileNameWithoutExtension(file.FullName);
			var fileDirectory = file.Directory;
			if (fileDirectory == null) continue;

			if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".gif" && extension != ".webp" && extension != ".webm" && extension != ".mp4") continue;

			if (!IsGuid(fileName)) {
				var newFileName = Guid.NewGuid().ToString("D");
				var newFile     = Path.Combine(fileDirectory.FullName, newFileName + file.Extension);
				Console.WriteLine($"Renaming {file.Name} to {newFile}");
				file.MoveTo(newFile);
				file.Refresh();
			}

			if (file.CreationTime != RefDate) file.CreationTime = RefDate;
			if (file.LastWriteTime != RefDate) file.LastWriteTime = RefDate;
			if (file.LastAccessTime != RefDate) file.LastAccessTime = RefDate;
		}
	}

	private static bool IsGuid(string input) {
		return guidRegex.IsMatch(input);
	}

	public static void WriteHeader() {
		Console.WriteLine("SynologyPhotos v1.0");
	}
}
