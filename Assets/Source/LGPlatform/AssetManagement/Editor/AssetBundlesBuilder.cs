// AssetBundlesBuilder.cs
// created by Alexander Shapoval
// e-mail: a.shapoval@twinwingames.com
// Copyright 2017 Luck Genome

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace LGPlatform.AssetManagement.Editor
{
    public sealed class AssetBundlesBuilder
    {
        private static readonly String AssetBundlesPath = $"{Application.streamingAssetsPath}/AssetBundles";

        [MenuItem("Tools/Asset Bundles/Rebuild bundles")]
        private static void RebuildBundles()
        {
            FileUtil.DeleteFileOrDirectory(AssetBundlesPath);
            Directory.CreateDirectory(AssetBundlesPath);

            Debug.Log(EditorUserBuildSettings.activeBuildTarget);

            BuildPipeline.BuildAssetBundles(AssetBundlesPath,
                EditorUserBuildSettings.activeBuildTarget == BuildTarget.WebGL
                    ? BuildAssetBundleOptions.ChunkBasedCompression
                    : BuildAssetBundleOptions.StrictMode, GetBuildTarget());
        }

        private static BuildTarget GetBuildTarget()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.StandaloneOSXUniversal:
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneWindows:
                case BuildTarget.iOS:
                case BuildTarget.Android:
                case BuildTarget.StandaloneOSXIntel64:
                case BuildTarget.StandaloneWindows64:
                case BuildTarget.WebGL:
                    return EditorUserBuildSettings.activeBuildTarget;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}