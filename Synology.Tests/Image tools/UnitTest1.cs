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

using System.Diagnostics;
using Synology.Foto;

namespace Synology.Image_tools;

[TestClass]
public class ImageTools {
	private static readonly DirectoryInfo TEST_DIRECTORY = new(Path.Combine(Settings.ImageBaseFolder.FullName, "A Test"));
	private static readonly DateTime REFERENCE_DATE = new DateTime(2000, 1, 1).Add(DateTime.Now.TimeOfDay);
	private static readonly string[] ADDITIONAL_TAGS = ["tag1", "tag2"];

	public TestContext TestContext { get; set; } = default!;

	public static IEnumerable<object[]> TestFiles() {
		foreach (var item in TEST_DIRECTORY.EnumerateFiles("*.*", SearchOption.AllDirectories)) yield return new object[] {item.FullName};
	}

	[DataTestMethod]
	[DynamicData(nameof(TestFiles), DynamicDataSourceType.Method)]
	public void ReadTags(string fullFileName) {
		foreach (var item in TEST_DIRECTORY.EnumerateFiles("*.*", SearchOption.AllDirectories)) {
			var tags = MediaHelper.GetTags(item);
			Console.WriteLine($"{item.Name} : {string.Join(',', tags)}");
		}
	}

	[DataTestMethod]
	[DynamicData(nameof(TestFiles), DynamicDataSourceType.Method)]
	public void ExifGet(string fullFileName) {
		var command = $"{Settings.Exiftool.FullName} -f -G \"{fullFileName}\"";

		// Set up process start info
		var startInfo = new ProcessStartInfo
		                {
			                FileName = "cmd.exe",        // Use cmd.exe on Windows
			                Arguments = $"/C {command}", // /C carries out the command specified by string and then terminates
			                RedirectStandardOutput = true,
			                UseShellExecute = false,
			                CreateNoWindow = true
		                };

		// Start the process
		using (var process = new Process()) {
			process.StartInfo = startInfo;
			process.Start();

			// Read the output (if needed)
			var output = process.StandardOutput.ReadToEnd();
			TestContext.WriteLine(output);

			process.WaitForExit();
		}
	}

	[DataTestMethod]
	[DynamicData(nameof(TestFiles), DynamicDataSourceType.Method)]
	public void SetTags(string fullFileName) {
		UpdateTagIfNeeded(fullFileName, ADDITIONAL_TAGS);
		ExifGet(fullFileName);
	}

	[TestMethod]
	public void ListOfFile() {
		foreach (var item in TEST_DIRECTORY.EnumerateFiles("*.*", SearchOption.AllDirectories))

			//var tags = MediaHelper.GetTags(item);
			Console.WriteLine(item.FullName);
	}

	[TestMethod]
	public async Task TestMethod1() {
		using var api    = await Settings.GetApi(TestContext);
		var       folder = await api.FotoBrowseFolderGet(2377);
		Assert.IsNotNull(folder);
		foreach (var photo in GetAllPhotos(api, folder.id, additional: ADDITIONAL_TAGS)) Process(folder, photo);
	}

	public static void Process(Folder folder, FotoItem photo) {
#if !true
		if (photo.filename?.Contains("temp.") == true) return;
		var initialName = Path.Combine("""\\192.168.0.200\Homes\m.f\Photos""" + (folder.name ?? string.Empty).Replace('/', '\\'), photo.filename ?? string.Empty);
		var newName = Path.GetFileNameWithoutExtension(photo.filename) + $"_{DateTime.Now:yyyyMMddHHmmss}_temp" + Path.GetExtension(photo.filename);
		var fullFileName = Path.Combine("""\\192.168.0.200\Homes\m.f\Photos""" + (folder.name ?? string.Empty).Replace('/', '\\'), newName);
		File.Copy(initialName, fullFileName);
#else
		var fullFileName = Path.Combine("""\\192.168.0.200\Homes\m.f\Photos""" + (folder.name ?? string.Empty).Replace('/', '\\'), photo.filename ?? string.Empty);
#endif
		var synoTags = new List<string>();
		if (folder.name != null) {
			foreach (var item in folder.name.Split('/')) {
				var tag = item.Trim();
				if (!string.IsNullOrWhiteSpace(tag) && !synoTags.Contains(tag)) synoTags.Add(tag);
			}
		}
		if (photo.additional?.tag != null) {
			foreach (var item in photo.additional.tag) {
				var tag = item.name?.Trim();
				if (!string.IsNullOrWhiteSpace(tag) && !synoTags.Contains(tag)) synoTags.Add(tag);
			}
		}
		UpdateTagIfNeeded(fullFileName, synoTags);
	}

	private static void UpdateTagIfNeeded(string fullFileName, IEnumerable<string> additionalTags) {
		var fileInfo = new FileInfo(fullFileName);
		var synoTags = additionalTags.ToList();

		if (!fileInfo.Exists) return;
		var exisitingTags = MediaHelper.GetTags(fileInfo);
		synoTags.Sort();
		exisitingTags.Sort();

		var allTags = new List<string>();
		foreach (var item in exisitingTags)
			if (!allTags.Contains(item))
				allTags.Add(item);
		foreach (var item in synoTags)
			if (!allTags.Contains(item))
				allTags.Add(item);

		CleanTag(allTags);

		// if (exisitingTags.Count == allTags.Count && string.Join('¤', allTags) == string.Join('¤', exisitingTags)) {
		// 	Console.WriteLine($"[Skip]{fileInfo.FullName}");
		// 	return;
		// }
		Console.WriteLine($"[Update]{fileInfo.FullName} - {string.Join(',', allTags)}");

		//Console.WriteLine($" * Syno Tags + Folder: {string.Join(',', synoTags)}");
		//Console.WriteLine($" * Existing tags     : {string.Join(',', exisitingTags)}");

		MediaHelper.SetTags(fileInfo, allTags);
		MediaHelper.SetDateTime(fileInfo.FullName, REFERENCE_DATE);
	}

	private static void CleanTag(ICollection<string> exisitingTags) {
		Add("dotNet test");
		Add("dotNet test 2");
		Remove("Composition");
		Remove("No_Face");
		Remove("noFace");
		Remove("Special");

		void Add(string tag) {
			if (!exisitingTags.Contains(tag)) exisitingTags.Add(tag);
		}

		void Remove(string tag) {
			exisitingTags.Remove(tag);
		}
	}

	private static List<FotoItem> GetAllPhotos(SynologyApi api, int? initialFolderId, int pageSize = 1000, params string[] additional) {
		var synoApi = api;
		var photos  = new List<FotoItem>();
		while (true) {
			try {
				var page      = synoApi.FotoBrowseItemList(photos.Count, pageSize, initialFolderId, additional: additional).GetAwaiter().GetResult();
				var fotoItems = page?.list;
				if (fotoItems is null) break;
				photos.AddRange(fotoItems);
				if (fotoItems.Count < pageSize) break;
			} catch (Exception e) {
				Console.WriteLine(e);
				break;
			}
		}
		return photos;
	}
}
