// <copyright>
// MIT License
// <author > Frederic Wauquier</author >
// </copyright >

namespace Synology.Foto;

public class FotoBrowseFolderGetResponse : JsonModel {
	public Folder? folder { get; init; }
}
