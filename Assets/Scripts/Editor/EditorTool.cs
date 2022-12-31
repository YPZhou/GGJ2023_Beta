using UnityEditor;

namespace GGJ2023.Beta
{
	public class EditorTool
	{
		[MenuItem("GGJ2023/Refresh Assets %#z")]
		public static void CompileScripts()
		{
			AssetDatabase.Refresh();
		}
	}
}