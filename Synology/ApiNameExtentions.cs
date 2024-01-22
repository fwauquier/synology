// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault

namespace Synology;

internal static class ApiNameExtensions {
	/// <summary>
	///     Get API name for Url. eg:SYNO.API.Info
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public static string GetApiName(this ApiName value) {
		switch (value) {
			case ApiName.SYNO_API_Auth_Key_Code:                      return "SYNO.API.Auth.Key.Code";
			case ApiName.SYNO_API_Auth_Key:                           return "SYNO.API.Auth.Key";
			case ApiName.SYNO_API_Auth_RedirectURI:                   return "SYNO.API.Auth.RedirectURI";
			case ApiName.SYNO_API_Auth_Type:                          return "SYNO.API.Auth.Type";
			case ApiName.SYNO_API_Auth_UIConfig:                      return "SYNO.API.Auth.UIConfig";
			case ApiName.SYNO_API_Auth:                               return "SYNO.API.Auth";
			case ApiName.SYNO_API_Info:                               return "SYNO.API.Info";
			case ApiName.SYNO_Foto_BackgroundTask_File:               return "SYNO.Foto.BackgroundTask.File";
			case ApiName.SYNO_Foto_BackgroundTask_Info:               return "SYNO.Foto.BackgroundTask.Info";
			case ApiName.SYNO_Foto_Browse_Album:                      return "SYNO.Foto.Browse.Album";
			case ApiName.SYNO_Foto_Browse_Category:                   return "SYNO.Foto.Browse.Category";
			case ApiName.SYNO_Foto_Browse_Concept:                    return "SYNO.Foto.Browse.Concept";
			case ApiName.SYNO_Foto_Browse_ConditionAlbum:             return "SYNO.Foto.Browse.ConditionAlbum";
			case ApiName.SYNO_Foto_Browse_Diff:                       return "SYNO.Foto.Browse.Diff";
			case ApiName.SYNO_Foto_Browse_Folder:                     return "SYNO.Foto.Browse.Folder";
			case ApiName.SYNO_Foto_Browse_GeneralTag:                 return "SYNO.Foto.Browse.GeneralTag";
			case ApiName.SYNO_Foto_Browse_Geocoding:                  return "SYNO.Foto.Browse.Geocoding";
			case ApiName.SYNO_Foto_Browse_Item:                       return "SYNO.Foto.Browse.Item";
			case ApiName.SYNO_Foto_Browse_NormalAlbum:                return "SYNO.Foto.Browse.NormalAlbum";
			case ApiName.SYNO_Foto_Browse_Person:                     return "SYNO.Foto.Browse.Person";
			case ApiName.SYNO_Foto_Browse_RecentlyAdded:              return "SYNO.Foto.Browse.RecentlyAdded";
			case ApiName.SYNO_Foto_Browse_Timeline:                   return "SYNO.Foto.Browse.Timeline";
			case ApiName.SYNO_Foto_Browse_Unit:                       return "SYNO.Foto.Browse.Unit";
			case ApiName.SYNO_Foto_Development_Admin:                 return "SYNO.Foto.Development.Admin";
			case ApiName.SYNO_Foto_Download:                          return "SYNO.Foto.Download";
			case ApiName.SYNO_Foto_Favorite:                          return "SYNO.Foto.Favorite";
			case ApiName.SYNO_Foto_Index:                             return "SYNO.Foto.Index";
			case ApiName.SYNO_Foto_Migration:                         return "SYNO.Foto.Migration";
			case ApiName.SYNO_Foto_Notification:                      return "SYNO.Foto.Notification";
			case ApiName.SYNO_Foto_PhotoRequest:                      return "SYNO.Foto.PhotoRequest";
			case ApiName.SYNO_Foto_PublicSharing:                     return "SYNO.Foto.PublicSharing";
			case ApiName.SYNO_Foto_Search_Filter:                     return "SYNO.Foto.Search.Filter";
			case ApiName.SYNO_Foto_Search_Search:                     return "SYNO.Foto.Search.Search";
			case ApiName.SYNO_Foto_Setting_Admin:                     return "SYNO.Foto.Setting.Admin";
			case ApiName.SYNO_Foto_Setting_Guest:                     return "SYNO.Foto.Setting.Guest";
			case ApiName.SYNO_Foto_Setting_MobileCompatibility:       return "SYNO.Foto.Setting.MobileCompatibility";
			case ApiName.SYNO_Foto_Setting_TeamSpace:                 return "SYNO.Foto.Setting.TeamSpace";
			case ApiName.SYNO_Foto_Setting_User:                      return "SYNO.Foto.Setting.User";
			case ApiName.SYNO_Foto_Setting_Wizard:                    return "SYNO.Foto.Setting.Wizard";
			case ApiName.SYNO_Foto_Sharing_Misc:                      return "SYNO.Foto.Sharing.Misc";
			case ApiName.SYNO_Foto_Sharing_Passphrase:                return "SYNO.Foto.Sharing.Passphrase";
			case ApiName.SYNO_Foto_Streaming:                         return "SYNO.Foto.Streaming";
			case ApiName.SYNO_Foto_Thumbnail:                         return "SYNO.Foto.Thumbnail";
			case ApiName.SYNO_Foto_Udc:                               return "SYNO.Foto.Udc";
			case ApiName.SYNO_Foto_Upload_ConvertedFile:              return "SYNO.Foto.Upload.ConvertedFile";
			case ApiName.SYNO_Foto_Upload_Item:                       return "SYNO.Foto.Upload.Item";
			case ApiName.SYNO_Foto_Upload_PhotoRequestItem:           return "SYNO.Foto.Upload.PhotoRequestItem";
			case ApiName.SYNO_Foto_UserInfo:                          return "SYNO.Foto.UserInfo";
			case ApiName.SYNO_FotoTeam_BackgroundTask_File:           return "SYNO.FotoTeam.BackgroundTask.File";
			case ApiName.SYNO_FotoTeam_Browse_Concept:                return "SYNO.FotoTeam.Browse.Concept";
			case ApiName.SYNO_FotoTeam_Browse_Diff:                   return "SYNO.FotoTeam.Browse.Diff";
			case ApiName.SYNO_FotoTeam_Browse_Folder:                 return "SYNO.FotoTeam.Browse.Folder";
			case ApiName.SYNO_FotoTeam_Browse_GeneralTag:             return "SYNO.FotoTeam.Browse.GeneralTag";
			case ApiName.SYNO_FotoTeam_Browse_Geocoding:              return "SYNO.FotoTeam.Browse.Geocoding";
			case ApiName.SYNO_FotoTeam_Browse_Item:                   return "SYNO.FotoTeam.Browse.Item";
			case ApiName.SYNO_FotoTeam_Browse_Person:                 return "SYNO.FotoTeam.Browse.Person";
			case ApiName.SYNO_FotoTeam_Browse_RecentlyAdded:          return "SYNO.FotoTeam.Browse.RecentlyAdded";
			case ApiName.SYNO_FotoTeam_Browse_Timeline:               return "SYNO.FotoTeam.Browse.Timeline";
			case ApiName.SYNO_FotoTeam_Browse_Unit:                   return "SYNO.FotoTeam.Browse.Unit";
			case ApiName.SYNO_FotoTeam_Download:                      return "SYNO.FotoTeam.Download";
			case ApiName.SYNO_FotoTeam_Index:                         return "SYNO.FotoTeam.Index";
			case ApiName.SYNO_FotoTeam_Search_Filter:                 return "SYNO.FotoTeam.Search.Filter";
			case ApiName.SYNO_FotoTeam_Search_Search:                 return "SYNO.FotoTeam.Search.Search";
			case ApiName.SYNO_FotoTeam_Sharing_FolderBatchPermission: return "SYNO.FotoTeam.Sharing.FolderBatchPermission";
			case ApiName.SYNO_FotoTeam_Sharing_FolderPermission:      return "SYNO.FotoTeam.Sharing.FolderPermission";
			case ApiName.SYNO_FotoTeam_Sharing_Passphrase:            return "SYNO.FotoTeam.Sharing.Passphrase";
			case ApiName.SYNO_FotoTeam_Streaming:                     return "SYNO.FotoTeam.Streaming";
			case ApiName.SYNO_FotoTeam_Thumbnail:                     return "SYNO.FotoTeam.Thumbnail";
			case ApiName.SYNO_FotoTeam_Upload_ConvertedFile:          return "SYNO.FotoTeam.Upload.ConvertedFile";
			case ApiName.SYNO_FotoTeam_Upload_Item:                   return "SYNO.FotoTeam.Upload.Item";
			default:                                                  throw new ArgumentOutOfRangeException(nameof(value), value, null);
		}
	}

	/// <summary>
	///     Return path for the API. In most of cases it's 'entry.cgi'
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static string GetPath(this ApiName value) {
		switch (value) {
			default: return "entry.cgi";
		}
	}

	public static string GetRequestFormat(this ApiName value) {
		switch (value) {
			case ApiName.SYNO_API_Auth: return "";
			default:                    return "JSON";
		}
	}

	public static int GetMaxVersion(this ApiName value) {
		switch (value) {
			case ApiName.SYNO_Foto_Browse_Category:
			case ApiName.SYNO_Foto_Browse_Concept:
			case ApiName.SYNO_Foto_Browse_Folder:
			case ApiName.SYNO_Foto_Browse_Person:
			case ApiName.SYNO_Foto_Browse_Unit:
			case ApiName.SYNO_Foto_Download:
			case ApiName.SYNO_Foto_Search_Filter:
			case ApiName.SYNO_Foto_Sharing_Misc:
			case ApiName.SYNO_Foto_Streaming:
			case ApiName.SYNO_Foto_Thumbnail:
			case ApiName.SYNO_FotoTeam_Browse_Concept:
			case ApiName.SYNO_FotoTeam_Browse_Folder:
			case ApiName.SYNO_FotoTeam_Browse_Person:
			case ApiName.SYNO_FotoTeam_Browse_Unit:
			case ApiName.SYNO_FotoTeam_Download:
			case ApiName.SYNO_FotoTeam_Search_Filter:
			case ApiName.SYNO_FotoTeam_Streaming:
			case ApiName.SYNO_FotoTeam_Thumbnail:
				return 2;
			case ApiName.SYNO_Foto_Browse_ConditionAlbum:
			case ApiName.SYNO_Foto_Browse_NormalAlbum:
				return 3;
			case ApiName.SYNO_Foto_Browse_Album:
			case ApiName.SYNO_Foto_Browse_Diff:
			case ApiName.SYNO_Foto_Browse_Item:
			case ApiName.SYNO_Foto_Browse_RecentlyAdded:
			case ApiName.SYNO_Foto_Browse_Timeline:
			case ApiName.SYNO_Foto_Upload_Item:
			case ApiName.SYNO_FotoTeam_Browse_Diff:
			case ApiName.SYNO_FotoTeam_Browse_Item:
			case ApiName.SYNO_FotoTeam_Browse_RecentlyAdded:
			case ApiName.SYNO_FotoTeam_Browse_Timeline:
			case ApiName.SYNO_FotoTeam_Upload_Item:
				return 5;
			case ApiName.SYNO_Foto_Search_Search:
			case ApiName.SYNO_FotoTeam_Search_Search:
				return 6;
			case ApiName.SYNO_API_Auth:
			case ApiName.SYNO_API_Auth_Key:
			case ApiName.SYNO_API_Auth_Key_Code:
				return 7;
			default: return 1;
		}
	}

	public static int GetMinVersion(this ApiName value) {
		switch (value) {
			case ApiName.SYNO_API_Auth_Key:
			case ApiName.SYNO_API_Auth_Key_Code:
				return 7;
			default: return 1;
		}
	}
}
