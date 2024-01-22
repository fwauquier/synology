// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

using System.Globalization;
using System.Text;

namespace Synology;

public class UrlParameter {
	private UrlParameter(string key, string? value) {
		Key = key;
		Value = value;
	}

	public string Key { get; }
	public string? Value { get; }

	public void AddTo(StringBuilder sb) {
		if (Value is null) return;
		sb.Append('&');
		sb.Append(Key);
		sb.Append('=');
		sb.Append(Value);
	}

	public static UrlParameter Get(string key, string? toEscape) {
		return new(key, toEscape is null ? null : Uri.EscapeDataString(toEscape));
	}

	public static UrlParameter Get(string key, int? value) {
		return new(key, value?.ToString("0", CultureInfo.InvariantCulture));
	}

	public static UrlParameter Get(string key, int value) {
		return new(key, value.ToString("0", CultureInfo.InvariantCulture));
	}

	public static UrlParameter Get(string key, IEnumerable<string?> toEscapes) {
		var join = string.Join("\",\"", toEscapes.Where(static i => !string.IsNullOrWhiteSpace(i)).Select(i => Uri.EscapeDataString(i!.Trim())));

		return new(key, string.IsNullOrWhiteSpace(join) ? null : "[\"" + join + "\"]");
	}
}
