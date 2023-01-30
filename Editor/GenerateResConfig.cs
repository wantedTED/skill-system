using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 1.Editor 2.MenuItem 3.AssetDatabase 4.StreamingAssets(特殊目录)
 */
public class GenerateResConfig : Editor
{
    [MenuItem("Tools/Resources/Generate ResConfig File")]
    public static void Generate() //生成资源配置文件（自动生成文件）
    {
        string[] resFiles = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Resources/SkillPrefab" });//获取GUID
        for (int i = 0; i < resFiles.Length; i++)
        {
            resFiles[i] = AssetDatabase.GUIDToAssetPath(resFiles[i]);//GUID装成文件路径
            string flieName = Path.GetFileNameWithoutExtension(resFiles[i]);
            string filePath = resFiles[i].Replace("Assets/Resources/", string.Empty).Replace(".prefab", string.Empty);
            resFiles[i] = flieName + "=" + filePath;
        }
        File.WriteAllLines("Assets/StreamingAssets/ConfigMap.txt", resFiles);//写入文件
        AssetDatabase.Refresh();//刷新
    }
}
