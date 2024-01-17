// MIT License
// Copyright (c) 2023 Frédéric Wauquier
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

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
		                                   new()
		                                   {
			                                   {"query", query},
		                                   })
			.ConfigureAwait(false);
		var result = GetAndValidate<Dictionary<string, ApiInfo>?>(json);
#if DEBUG

		// Ensure all JSON field deserialized
		if (result != null)
			foreach (var item in result.Values)
				item.EnsureAllDataDeserialized(json);
#endif
		return result;
	}
}
