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

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Synology;

public class JsonModel {
	/// <summary>
	///     Additional properties
	/// </summary>
	[JsonExtensionData]

	// ReSharper disable once CollectionNeverUpdated.Global
	public Dictionary<string, object>? AdditionalProperties { get; init; }

#if DEBUG
	private static readonly JsonSerializerOptions jsonOptionIndented = new()
	                                                                  {
		                                                                  WriteIndented = true,
		                                                                  IgnoreReadOnlyFields = true,
		                                                                  IgnoreReadOnlyProperties = true,
		                                                                  DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
	                                                                  };

	/// <summary>
	/// Serialize object to JSON
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public static string Serialize(object? obj) {
		return JsonSerializer.Serialize(obj, jsonOptionIndented);
	}

	/// <summary>
	/// Reformat code to be more readable
	/// </summary>
	/// <param name="json"></param>
	/// <returns></returns>
	public static string Reformat(string json) {
		var utf8Json = JsonSerializer.Deserialize<object>(json);
		return JsonSerializer.Serialize(utf8Json, jsonOptionIndented);
	}

	/// <summary>
	/// Ensure all JSON properties deserialized on own property (<see cref="AdditionalProperties"/> must be empty)
	/// </summary>
	/// <exception cref="ApiException"></exception>
	public void EnsureAllDataDeserialized() {
		var model = this;
		if (model.AdditionalProperties is null) return;
		var type = model.GetType();
		if (model.AdditionalProperties.Count > 0) throw new ApiException($"Some properties not deserialized for type {type.FullName}.{Environment.NewLine}{Serialize(model.AdditionalProperties)}");
		foreach (var property in type.GetProperties()) {
			var value = property.GetValue(model);
			switch (value) {
				case null: continue;
				case JsonModel jsonModel:
					jsonModel.EnsureAllDataDeserialized();
					break;
				case IEnumerable<JsonModel> jsonModels: {
					foreach (var jsonModel2 in jsonModels) jsonModel2.EnsureAllDataDeserialized();
					break;
				}
			}
		}
	}
#endif
}
