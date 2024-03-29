﻿// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

// ReSharper disable UnusedMember.Global

using System.Text;
using Synology.Foto;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Synology;

public sealed partial class SynologyApi {
	// private Login? m_FotoLogin;
	//
	// /// <summary>
	// ///     Login to the server
	// /// </summary>
	// public bool IsFotoLoggedIn => m_FotoLogin is not null;
	//
	// [MemberNotNull(nameof(m_FotoLogin))]
	// public void EnsureFotoLoggedIn() {
	// 	if (m_FotoLogin is null) throw new Exception($"Not logged in to {Server} with photo login");
	// }
	//
	// public void EnsureFotoLoggedOut() {
	// 	if (IsLoggedIn) FotoLogOut();
	// }
	//
	// public async Task<bool> FotoLogin(string user, string password) {
	// 	var apiUrl =
	// 		$"{Server.Replace(":5001","")}/photo/webapi/auth.cgi?api=SYNO.API.Auth&version=3&method=loginaccount={Uri.EscapeDataString(user)}&passwd={Uri.EscapeDataString(password)}&enable_syno_token=yes";
	// 	var result = await ValidateLogin(apiUrl).ConfigureAwait(false);
	// 	m_FotoLogin = result.data;
	// 	return m_FotoLogin is not null;
	// }
	//
	// private void FotoLogOut() {
	// 	EnsureFotoLoggedIn();
	// 	var apiUrl = $"{Server}/photo/webapi/auth.cgi?api=SYNO.API.Auth&version=3&method=logout";
	// 	_ = GetResultAsString(apiUrl).ConfigureAwait(false);
	// 	m_FotoLogin = null;
	// }

	/// <summary>
	///     List all items in all folders in Personal Space.
	/// </summary>
	/// <param name="offset">Required. Specify how many shared folders are skipped before beginning to return listed shared folders.</param>
	/// <param name="limit">Required. Number of shared folders requested. 0 lists all shared folders.</param>
	/// <param name="folder_id">Optional. ID of folder</param>
	/// <param name="sort_by">
	///     Optional. Only working, when folder_id is given.<br /> Specify which file information to sort on.
	///     <ul>Options:<li>filename: file name</li><li>filesize: file size</li><li>takentime: time of taking image</li><li>item_type: type of the item</li></ul>
	/// </param>
	/// <param name="sort_direction">
	///     Optional. Only working, when folder_id is given.<br /> Specify to sort ascending or to sort descending.
	///     <ul>Options include:<li>asc: sort ascending.</li><li>desc: sort descending.</li></ul>
	/// </param>
	/// <param name="type">
	///     <ul>Type of data<li>photo: Photo</li><li>video: Video</li><li>live: iPhone live photos</li></ul>
	/// </param>
	/// <param name="passphrase">Passphrase for a shared album, i.e. xXHXZxIov. Only items of this album will be listed.</param>
	/// <param name="additional">
	///     Get additional information about an item.
	///     <ul>
	///         <li>thumbnail: Available thumbnail sizes. Values can be sm, m, xl.</li>
	///         <li>resolution: returns height and width of original photo</li>
	///         <li>orientation: Orientation of file. Value seems to be integer.</li>
	///         <li>video_convert: ???</li>
	///         <li>video_meta: ???</li>
	///         <li>provider_user_id: ???</li>
	///         <li>exif: Returns keys as aperture, camera, exposure_time", focal_length, iso, lens.</li>
	///         <li>tag: Returns keys id and name for every tag.</li>
	///         <li>description: Returns exif description.</li>
	///         <li>gps: Coordinates returned as values latitude and longitude.</li>
	///         <li>geocoding_id: Geocoding id for internal database ???</li>
	///         <li>address: Address calculated from gps coordinates.</li>
	///         <li>person: Returns person object.</li>
	///     </ul>
	///     Example: "thumbnail","resolution"
	/// </param>
	/// <returns></returns>
	public async Task<FotoItem.List?> FotoBrowseItemList(int offset = 0,
		int limit = 100,
		int? folder_id = null,
		string? sort_by = null,
		string? sort_direction = null,
		string? type = null,
		string? passphrase = null,
		params string[] additional) {
		EnsureLoggedIn();
		var uri = new StringBuilder($"{Server}/webapi/entry.cgi?api=SYNO.Foto.Browse.Item&version=1&method=list&offset={offset}&limit={limit}");
		if (folder_id is not null) uri.Append($"&folder_id={folder_id}");
		if (!string.IsNullOrEmpty(sort_by)) uri.Append($"&sort_by={sort_by}");
		if (!string.IsNullOrEmpty(sort_direction)) uri.Append($"&sort_direction={sort_direction}");
		if (!string.IsNullOrEmpty(type)) uri.Append($"&type={type}");
		if (!string.IsNullOrEmpty(passphrase)) uri.Append($"&passphrase={passphrase}");

		var json = await GetResultAsString(ApiName.SYNO_Foto_Browse_Item,
		                                   "list",
		                                   [
			                                   UrlParameter.Get("offset", offset.ToString()),
			                                   UrlParameter.Get("limit", limit.ToString()),
			                                   UrlParameter.Get("folder_id", folder_id?.ToString()),
			                                   UrlParameter.Get("sort_by", sort_by),
			                                   UrlParameter.Get("sort_direction", sort_direction),
			                                   UrlParameter.Get("type", type),
			                                   UrlParameter.Get("passphrase", passphrase),
			                                   UrlParameter.Get("additional", additional)
		                                   ])
			.ConfigureAwait(false);

		return GetAndValidate<FotoItem.List?>(json);
	}

	/// <summary>
	///     List all items in all folders in Personal Space.
	/// </summary>
	/// <param name="offset">Required. Specify how many shared folders are skipped before beginning to return listed shared folders.</param>
	/// <param name="limit">Required. Number of shared folders requested. 0 lists all shared folders.</param>
	/// <param name="id">Optional. ID of parent directory</param>
	/// <param name="sort_by">
	///     Optional. Only working, when folder_id is given.<br /> Specify which file information to sort on.
	///     <ul>
	///         Options:<li>name: file name.</li><li>size: file size.</li><li>user: file owner.</li><li>group: file group.</li><li>mtime: last modified time.</li><li>atime: last access time.</li><li>ctime: last change time.</li>
	///         <li>crtime: create time.</li><li>posix: POSIX permission.</li><li>type: file extension.</li>
	///     </ul>
	/// </param>
	/// <param name="sort_direction">
	///     Optional. Only working, when folder_id is given.<br /> Specify to sort ascending or to sort descending.
	///     <ul>Options include:<li>asc: sort ascending.</li><li>desc: sort descending.</li></ul>
	/// </param>
	/// <returns></returns>
	public async Task<Folder[]?> FotoBrowseFolderList(int offset = 0,
		int limit = 100,
		int? id = null,
		string? sort_by = null,
		string? sort_direction = null) {
		EnsureLoggedIn();

		var resultAsString = await GetResultAsString(ApiName.SYNO_Foto_Browse_Folder,
		                                             "list",
		                                             [
			                                             UrlParameter.Get("offset", offset),
			                                             UrlParameter.Get("limit", limit),
			                                             UrlParameter.Get("id", id),
			                                             UrlParameter.Get("sort_by", sort_by),
			                                             UrlParameter.Get("sort_direction", sort_direction)
		                                             ])
			.ConfigureAwait(false);
		return GetAndValidate<Folder.List>(resultAsString)?.list;
	}

	public async Task<Folder?> FotoBrowseFolderGet(int? id) {
		EnsureLoggedIn();
		var json = await GetResultAsString(ApiName.SYNO_Foto_Browse_Folder,
		                                   "get",
		                                   [UrlParameter.Get("id", id)])
			.ConfigureAwait(false);
		return GetAndValidate<FotoBrowseFolderGetResponse>(json)?.folder;
	}
}
