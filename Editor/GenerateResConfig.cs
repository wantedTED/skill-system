using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 1.Editor 2.MenuItem 3.AssetDatabase 4.StreamingAssets(����Ŀ¼)
 */
public class GenerateResConfig : Editor
{
    [MenuItem("Tools/Resources/Generate ResConfig File")]
    public static void Generate() //������Դ�����ļ����Զ������ļ���
    {
        string[] resFiles = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Resources/SkillPrefab" });//��ȡGUID
        for (int i = 0; i < resFiles.Length; i++)
        {
            resFiles[i] = AssetDatabase.GUIDToAssetPath(resFiles[i]);//GUIDװ���ļ�·��
            string flieName = Path.GetFileNameWithoutExtension(resFiles[i]);
            string filePath = resFiles[i].Replace("Assets/Resources/", string.Empty).Replace(".prefab", string.Empty);
            resFiles[i] = flieName + "=" + filePath;
        }
        File.WriteAllLines("Assets/StreamingAssets/ConfigMap.txt", resFiles);//д���ļ�
        AssetDatabase.Refresh();//ˢ��
    }
}
