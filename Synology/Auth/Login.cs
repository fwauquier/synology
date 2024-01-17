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

namespace Synology.Auth;

/// <summary>
/// Login response
/// </summary>
public class Login : JsonModel {
	/// <summary>
	/// A.k.a. device id, to identify which device.
	/// </summary>
	public string? did { get; init; }
	/// <summary>
	/// Authorized session ID.
	/// </summary>
	/// <remarks>When the user log in with format=sid, cookie will not be set and each API request shouldprovide a request parameter _sid=&lt;sid&gt; along with other parameters.</remarks>
	public string? sid { get; init; }
	/// <summary>
	/// Irrelevant.
	/// </summary>
	public bool? is_portal_port { get; init; }
	/// <summary>
	/// This token avoids Cross-Site RequestForgery.
	/// </summary>
	/// <remarks>Each subsequent API request should provide arequest parameter SynoToken=&lt;synotoken&gt; along with other parameters.</remarks>
	public string? synotoken { get; init; }


	public string? account{ get; init; }
	public string? device_id{ get; init; }
	public string? ik_message{ get; init; }
}
