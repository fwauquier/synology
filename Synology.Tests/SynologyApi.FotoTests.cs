﻿// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

using Microsoft.Extensions.Logging;
using Synology.Foto;

// ReSharper disable AccessToDisposedClosure

namespace Synology;

[TestClass]
public class SynologyAPI_Foto_Tests {
	private static readonly string[] ALL_ADDITIONALS =
	[
		"thumbnail",
		"resolution",
		"orientation",
		"video_convert",
		"video_meta",
		"provider_user_id",
		"exif",
		"tag",
		"description",
		"gps",
		"geocoding_id",
		"address",
		"person"
	];

	public TestContext TestContext { get; set; } = default!;

	//
	// [TestMethod] public async Task GenerateTestMethods( ) {
	// 	using var api  = await Settings.GetApi(TestContext, false,LogLevel.Warning);
	// 	var       data =await api.GetApiInfoMethods("SYNO.Foto.Browse.");
	// 	Assert.IsNotNull(data);
	//
	// 	var methods=GetType().GetMethods().Select(i=>i.Name).ToList();
	// 	//methods.Dump();
	//
	//
	// 	 foreach(var item in data.Keys) {
	// 		 var methodName= item.Replace(".","_");
	// 		 TestContext.WriteLine(methodName);
	// 	// 	TestContext.WriteLine($"[TestMethod]\r\npublic async Task {item.key}() {{\r\nusing var api = await Settings.GetApi(TestContext, true);\r\nvar data = await api.{item.key}();\r\nAssert.IsNotNull(data);\r\n}}");
	// 	 }
	// 	 data.Dump();
	// }

	[TestMethod]
	public async Task FoldersAll() {
		using var api = await Settings.GetApi(TestContext, true, LogLevel.Information);
		await ProcessFolder(null);

		async Task ProcessFolder(int? id) {
			var folders = await api.FotoBrowseFolderList(limit: 1000, id: id);
			if (folders is null) return;
			foreach (var folder in folders) {
				TestContext.WriteLine($"{folder.name} ({folder.id})");
				await ProcessFolder(folder.id);
			}
		}
	}

	[DataTestMethod]
	[DataRow(null)]
	[DataRow(892)]
	[DataRow(2377)]
	public async Task ItemAll(int? folderId) {
		using var api = await Settings.GetApi(TestContext, true, LogLevel.Information);

		var photos = GetAllPhotos(api, folderId);
		TestContext.WriteLine($"Number of items found : {photos.Count} ");

		foreach (var item in photos) TestContext.WriteLine($"{item.filename} ({item.id}) - folderId:{item.folder_id} ");
	}

	public static List<FotoItem> GetAllPhotos(SynologyApi api,
		int? initialFolderId,
		int limit = 1000,
		params string[] additional) {
		var synoApi = api;
		var photos  = new List<FotoItem>();
		while (true) {
			try {
				var page      = synoApi.FotoBrowseItemList(photos.Count, limit, initialFolderId, additional: additional).GetAwaiter().GetResult();
				var fotoItems = page?.list;
				if (fotoItems is null) break;
				photos.AddRange(fotoItems);
				if (fotoItems.Length < limit) break;
			} catch (Exception e) {
				Console.WriteLine(e);
				break;
			}
		}
		return photos;
	}

	[TestMethod]
	public async Task FolderFirst() {
		using var api   = await Settings.GetApi(TestContext);
		var       all   = await api.FotoBrowseFolderList(limit: 1);
		var       first = all?.FirstOrDefault();
		Assert.IsNotNull(first);
		first.DumpAll();
	}

	[DataTestMethod]
	[DataRow(null)]
	[DataRow(892)]
	[DataRow(2377)]
	[DataRow(1031)]
	public async Task FolderGet(int? folderId) {
		using var api  = await Settings.GetApi(TestContext, true, LogLevel.Trace);
		var       item = await api.FotoBrowseFolderGet(folderId);

		Assert.IsNotNull(item);
		item.DumpAll();
	}

	[TestMethod]
	public async Task FirstFile() {
		using var api = await Settings.GetApi(TestContext, true, LogLevel.Trace);

		var all = await api.FotoBrowseItemList(limit: 1,
		                                       additional: ALL_ADDITIONALS);
		var first = all?.list?.FirstOrDefault();
		Assert.IsNotNull(first);
		first.DumpAll();
	}
}
