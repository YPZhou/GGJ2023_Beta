using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class DefaultOpen : MonoBehaviour
{
    [OnOpenAsset(0)]
    public static bool OpenAsset(int instanceID, int line)
    {
        string filePath = AssetDatabase.GetAssetPath(instanceID);
        string fileName = System.IO.Directory.GetParent(Application.dataPath).ToString() + "/" + filePath;

        if (fileName.EndsWith(".shader"))
        {
            string editorPath = Environment.GetEnvironmentVariable("VSCode_Path");
            if (editorPath != null && editorPath.Length > 0)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = editorPath + (editorPath.EndsWith("/") ? "" : "/") + "Code.exe";   //你的文件名字
                startInfo.Arguments = "\"" + fileName + "\"";
                process.StartInfo = startInfo;
                process.Start();
                return true;
            }
            else
            {
                Debug.LogError("Can not Find Environment : VSCode_Path");
                return false;
            }
        }
        return false;
    }
}
