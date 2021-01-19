using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class AssetBundle : Editor
{
    [MenuItem("Assets/Export Asset Bundle")]
    static void CreateThis()
    {
        string path = @"V:\work\LAUNCH SHIP OFFICE\launchShipTrain\AssetBundle";
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);
    }


}//class
