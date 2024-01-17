// MIT License
// Copyright (c) 2023 Frédéric Wauquier
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

// using System.Diagnostics.CodeAnalysis;
// using Synology;
// using Synology.Foto;
//
// namespace SynologyPhotos;
//
// public sealed class MfTools : IDisposable {
// 	private SynoApi? m_Api;
// 	private static string SynoServer { get; } = "http://192.168.0.200:5000";
// 	private static string SynoMFUser { get; } = "m.f";
// 	private static string SynoMFPass { get; } = "2BmGzQ42lN&RFvMX";
// 	private static DirectoryInfo BaseFolder { get; } = new DirectoryInfo("""\\192.168.0.200\Homes\m.f\Photos""");
//
// 	void IDisposable.Dispose() {
// 		m_Api?.Dispose();
// 		m_Api = null;
// 	}
//
// 	[MemberNotNull(nameof(m_Api))]
// 	private void EnsureLogged() {
// 		if (m_Api is null) {
// 			m_Api = new SynoApi(SynoServer);
// 			m_Api.Login(SynoMFUser, SynoMFPass).Wait();
// 		}
// 	}
//
//
//
//
//
//
// //await api.GetApiInfoMethods("SYNO.Foto.").DumpAsync();
//
// //await api.FotoLogin(Settings.SynoMFUser, Settings.SynoMFPass).DumpAsync();
//
// //
// // var photos = new List<FotoItem>();
// //
// // /*
// //  {
// //                 "id": 869,
// //                 "name": "/TitsJob",
// //                 "owner_user_id": 3,
// //                 "parent": 470,
// //                 "passphrase": "",
// //                 "shared": false,
// //                 "sort_by": "default",
// //                 "sort_direction": "default"
// //             },
// //  */
// // while (true) {
// // 	try {
// // 		const int limit = 1000;
// // 		var page      = await api.FotoBrowseItemList(offset: photos.Count, limit: limit, folder_id:884, additional: new[] {"tag"}).ConfigureAwait(false);
// // 		var fotoItems = page?.list;
// // 		if (fotoItems is null) break;
// // 		photos.AddRange(fotoItems);
// // 		if(fotoItems.Count < limit) break;
// //
// //
// // 	} catch (Exception e) {
// // 		Console.WriteLine(e);
// // 		break;
// // 	}
// // }
// // foreach (var photo in photos) {
// // 	Console.WriteLine($"Photo: {photo.filename} - Tags: {string.Join(',', photo.additional?.tag?.Select(static i => i.name) ?? Array.Empty<string>())}");
// // }
//
// //await api.FotoBrowseFolderList(limit:1000).DumpAsync();
//
// 	public static void Process(Folder folder, FotoItem photo) {
// #if true
// 		if (photo.filename?.Contains("temp.") == true) return;
// 		var initialName  = Path.Combine("""\\192.168.0.200\Homes\m.f\Photos""" + (folder.name ?? string.Empty).Replace('/', '\\'), photo.filename ?? string.Empty);
// 		var newName      = Path.GetFileNameWithoutExtension(photo.filename) + $"_{DateTime.Now:yyyyMMddHHmmss}_temp" + Path.GetExtension(photo.filename);
// 		var fullFileName = Path.Combine("""\\192.168.0.200\Homes\m.f\Photos""" + (folder.name ?? string.Empty).Replace('/', '\\'), newName);
// 		File.Copy(initialName, fullFileName);
// #else
// 		var fullFileName = Path.Combine("""\\192.168.0.200\Homes\m.f\Photos"""+( folder.name??string.Empty).Replace('/','\\'), photo.filename??string.Empty);
// #endif
// 		var fileInfo = new FileInfo(fullFileName);
//
// 		var tags = new List<string>();
// 		if (folder.name != null) {
// 			foreach (var item in folder.name.Split('/')) {
// 				var tag = item.Trim();
// 				if (!string.IsNullOrWhiteSpace(tag) && !tags.Contains(tag)) tags.Add(tag);
// 			}
// 		}
// 		if (photo.additional?.tag != null) {
// 			foreach (var item in photo.additional.tag) {
// 				var tag = item.name?.Trim();
// 				if (!string.IsNullOrWhiteSpace(tag) && !tags.Contains(tag)) tags.Add(tag);
// 			}
// 		}
// 		tags.Sort();
// 		Console.WriteLine($"Folder: {folder.name} ({folder.id})/{photo.filename} - Syno Tags + Folder: {string.Join(',', tags)}");
// 		if (!fileInfo.Exists) return;
// 	}
// }


