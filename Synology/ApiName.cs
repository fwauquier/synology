// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

namespace Synology;

internal enum ApiName {
	/// <summary>	API:SYNO.API.Auth path:entry.cgi version:1 to 7</summary>
	SYNO_API_Auth,

	/// <summary>	API:SYNO.API.Auth.Key path:entry.cgi version:7 to 7</summary>
	SYNO_API_Auth_Key,

	/// <summary>	API:SYNO.API.Auth.Key.Code path:entry.cgi version:7 to 7</summary>
	SYNO_API_Auth_Key_Code,

	/// <summary>	API:SYNO.API.Auth.RedirectURI path:entry.cgi version:1 to 1</summary>
	SYNO_API_Auth_RedirectURI,

	/// <summary>	API:SYNO.API.Auth.Type path:entry.cgi version:1 to 1</summary>
	SYNO_API_Auth_Type,

	/// <summary>	API:SYNO.API.Auth.UIConfig path:entry.cgi version:1 to 1</summary>
	SYNO_API_Auth_UIConfig,

	/// <summary>	API:SYNO.API.Info path:entry.cgi version:1 to 1</summary>
	SYNO_API_Info,

	/// <summary>	API:SYNO.Foto.BackgroundTask.File path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_BackgroundTask_File,

	/// <summary>	API:SYNO.Foto.BackgroundTask.Info path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_BackgroundTask_Info,

	/// <summary>	API:SYNO.Foto.Browse.Album path:entry.cgi version:1 to 5</summary>
	SYNO_Foto_Browse_Album,

	/// <summary>	API:SYNO.Foto.Browse.Category path:entry.cgi version:1 to 2</summary>
	SYNO_Foto_Browse_Category,

	/// <summary>	API:SYNO.Foto.Browse.Concept path:entry.cgi version:1 to 2</summary>
	SYNO_Foto_Browse_Concept,

	/// <summary>	API:SYNO.Foto.Browse.ConditionAlbum path:entry.cgi version:1 to 3</summary>
	SYNO_Foto_Browse_ConditionAlbum,

	/// <summary>	API:SYNO.Foto.Browse.Diff path:entry.cgi version:1 to 5</summary>
	SYNO_Foto_Browse_Diff,

	/// <summary>	API:SYNO.Foto.Browse.Folder path:entry.cgi version:1 to 2</summary>
	SYNO_Foto_Browse_Folder,

	/// <summary>	API:SYNO.Foto.Browse.GeneralTag path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Browse_GeneralTag,

	/// <summary>	API:SYNO.Foto.Browse.Geocoding path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Browse_Geocoding,

	/// <summary>	API:SYNO.Foto.Browse.Item path:entry.cgi version:1 to 5</summary>
	SYNO_Foto_Browse_Item,

	/// <summary>	API:SYNO.Foto.Browse.NormalAlbum path:entry.cgi version:1 to 3</summary>
	SYNO_Foto_Browse_NormalAlbum,

	/// <summary>	API:SYNO.Foto.Browse.Person path:entry.cgi version:1 to 2</summary>
	SYNO_Foto_Browse_Person,

	/// <summary>	API:SYNO.Foto.Browse.RecentlyAdded path:entry.cgi version:1 to 5</summary>
	SYNO_Foto_Browse_RecentlyAdded,

	/// <summary>	API:SYNO.Foto.Browse.Timeline path:entry.cgi version:1 to 5</summary>
	SYNO_Foto_Browse_Timeline,

	/// <summary>	API:SYNO.Foto.Browse.Unit path:entry.cgi version:1 to 2</summary>
	SYNO_Foto_Browse_Unit,

	/// <summary>	API:SYNO.Foto.Development.Admin path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Development_Admin,

	/// <summary>	API:SYNO.Foto.Download path:entry.cgi version:1 to 2</summary>
	SYNO_Foto_Download,

	/// <summary>	API:SYNO.Foto.Favorite path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Favorite,

	/// <summary>	API:SYNO.Foto.Index path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Index,

	/// <summary>	API:SYNO.Foto.Migration path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Migration,

	/// <summary>	API:SYNO.Foto.Notification path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Notification,

	/// <summary>	API:SYNO.Foto.PhotoRequest path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_PhotoRequest,

	/// <summary>	API:SYNO.Foto.PublicSharing path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_PublicSharing,

	/// <summary>	API:SYNO.Foto.Search.Filter path:entry.cgi version:1 to 2</summary>
	SYNO_Foto_Search_Filter,

	/// <summary>	API:SYNO.Foto.Search.Search path:entry.cgi version:1 to 6</summary>
	SYNO_Foto_Search_Search,

	/// <summary>	API:SYNO.Foto.Setting.Admin path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Setting_Admin,

	/// <summary>	API:SYNO.Foto.Setting.Guest path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Setting_Guest,

	/// <summary>	API:SYNO.Foto.Setting.MobileCompatibility path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Setting_MobileCompatibility,

	/// <summary>	API:SYNO.Foto.Setting.TeamSpace path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Setting_TeamSpace,

	/// <summary>	API:SYNO.Foto.Setting.User path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Setting_User,

	/// <summary>	API:SYNO.Foto.Setting.Wizard path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Setting_Wizard,

	/// <summary>	API:SYNO.Foto.Sharing.Misc path:entry.cgi version:1 to 2</summary>
	SYNO_Foto_Sharing_Misc,

	/// <summary>	API:SYNO.Foto.Sharing.Passphrase path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Sharing_Passphrase,

	/// <summary>	API:SYNO.Foto.Streaming path:entry.cgi version:1 to 2</summary>
	SYNO_Foto_Streaming,

	/// <summary>	API:SYNO.Foto.Thumbnail path:entry.cgi version:1 to 2</summary>
	SYNO_Foto_Thumbnail,

	/// <summary>	API:SYNO.Foto.Udc path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Udc,

	/// <summary>	API:SYNO.Foto.Upload.ConvertedFile path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Upload_ConvertedFile,

	/// <summary>	API:SYNO.Foto.Upload.Item path:entry.cgi version:1 to 5</summary>
	SYNO_Foto_Upload_Item,

	/// <summary>	API:SYNO.Foto.Upload.PhotoRequestItem path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_Upload_PhotoRequestItem,

	/// <summary>	API:SYNO.Foto.UserInfo path:entry.cgi version:1 to 1</summary>
	SYNO_Foto_UserInfo,

	/// <summary>	API:SYNO.FotoTeam.BackgroundTask.File path:entry.cgi version:1 to 1</summary>
	SYNO_FotoTeam_BackgroundTask_File,

	/// <summary>	API:SYNO.FotoTeam.Browse.Concept path:entry.cgi version:1 to 2</summary>
	SYNO_FotoTeam_Browse_Concept,

	/// <summary>	API:SYNO.FotoTeam.Browse.Diff path:entry.cgi version:1 to 5</summary>
	SYNO_FotoTeam_Browse_Diff,

	/// <summary>	API:SYNO.FotoTeam.Browse.Folder path:entry.cgi version:1 to 2</summary>
	SYNO_FotoTeam_Browse_Folder,

	/// <summary>	API:SYNO.FotoTeam.Browse.GeneralTag path:entry.cgi version:1 to 1</summary>
	SYNO_FotoTeam_Browse_GeneralTag,

	/// <summary>	API:SYNO.FotoTeam.Browse.Geocoding path:entry.cgi version:1 to 1</summary>
	SYNO_FotoTeam_Browse_Geocoding,

	/// <summary>	API:SYNO.FotoTeam.Browse.Item path:entry.cgi version:1 to 5</summary>
	SYNO_FotoTeam_Browse_Item,

	/// <summary>	API:SYNO.FotoTeam.Browse.Person path:entry.cgi version:1 to 2</summary>
	SYNO_FotoTeam_Browse_Person,

	/// <summary>	API:SYNO.FotoTeam.Browse.RecentlyAdded path:entry.cgi version:1 to 5</summary>
	SYNO_FotoTeam_Browse_RecentlyAdded,

	/// <summary>	API:SYNO.FotoTeam.Browse.Timeline path:entry.cgi version:1 to 5</summary>
	SYNO_FotoTeam_Browse_Timeline,

	/// <summary>	API:SYNO.FotoTeam.Browse.Unit path:entry.cgi version:1 to 2</summary>
	SYNO_FotoTeam_Browse_Unit,

	/// <summary>	API:SYNO.FotoTeam.Download path:entry.cgi version:1 to 2</summary>
	SYNO_FotoTeam_Download,

	/// <summary>	API:SYNO.FotoTeam.Index path:entry.cgi version:1 to 1</summary>
	SYNO_FotoTeam_Index,

	/// <summary>	API:SYNO.FotoTeam.Search.Filter path:entry.cgi version:1 to 2</summary>
	SYNO_FotoTeam_Search_Filter,

	/// <summary>	API:SYNO.FotoTeam.Search.Search path:entry.cgi version:1 to 6</summary>
	SYNO_FotoTeam_Search_Search,

	/// <summary>	API:SYNO.FotoTeam.Sharing.FolderBatchPermission path:entry.cgi version:1 to 1</summary>
	SYNO_FotoTeam_Sharing_FolderBatchPermission,

	/// <summary>	API:SYNO.FotoTeam.Sharing.FolderPermission path:entry.cgi version:1 to 1</summary>
	SYNO_FotoTeam_Sharing_FolderPermission,

	/// <summary>	API:SYNO.FotoTeam.Sharing.Passphrase path:entry.cgi version:1 to 1</summary>
	SYNO_FotoTeam_Sharing_Passphrase,

	/// <summary>	API:SYNO.FotoTeam.Streaming path:entry.cgi version:1 to 2</summary>
	SYNO_FotoTeam_Streaming,

	/// <summary>	API:SYNO.FotoTeam.Thumbnail path:entry.cgi version:1 to 2</summary>
	SYNO_FotoTeam_Thumbnail,

	/// <summary>	API:SYNO.FotoTeam.Upload.ConvertedFile path:entry.cgi version:1 to 1</summary>
	SYNO_FotoTeam_Upload_ConvertedFile,

	/// <summary>	API:SYNO.FotoTeam.Upload.Item path:entry.cgi version:1 to 5</summary>
	SYNO_FotoTeam_Upload_Item
}
