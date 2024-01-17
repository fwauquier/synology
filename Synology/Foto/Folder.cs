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

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Synology.Foto;

public sealed class Folder : JsonModel {
	/// <summary> ID of folder  </summary>
	public int id { get; init; }

	/// <summary> Folder name  </summary>
	public string? name { get; init; }

	/// <summary>  UID of album owner </summary>
	public int owner_user_id { get; init; }

	/// <summary>  ID of parent directory </summary>
	public int parent { get; init; }

	/// <summary> Passphrase  </summary>
	public string? passphrase { get; init; }

	/// <summary>   </summary>
	public bool shared { get; init; }

	/// <summary>   </summary>
	public string? sort_by { get; init; }

	/// <summary>   </summary>
	public string? sort_direction { get; init; }

	// ReSharper disable once ClassNeverInstantiated.Global
	internal sealed class List : JsonModel {
		/// <summary> List of items </summary>
		public Folder[]? list { get; init; }
	}
}
