// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

// ReSharper disable UnusedMember.Global

using Synology.Info;

namespace Synology;

public sealed partial class SynologyApi {
	/// <summary>
	/// </summary>
	/// <param name="query">API names, separated by a comma "," or use "all" to get all supported APIs. eg:SYNO.Foto.,SYNO.FotoTeam.,SYNO.API.Auth,SYNO.FileStation</param>
	/// <returns></returns>
	public async Task<Dictionary<string, ApiInfo>?> GetApiInfoMethods(string query = "all") {
		var json = await GetResultAsString(ApiName.SYNO_API_Info,
		                                   "query",
		                                   [UrlParameter.Get("query", query)])
			.ConfigureAwait(false);
		var result = GetAndValidate<Dictionary<string, ApiInfo>?>(json);
#if DEBUG

		// Ensure all JSON field deserialized
		if (result != null)
			foreach (var item in result.Values)
				item.EnsureAllDataDeserialized();
#endif
		return result;
	}
}
