// LocalBundleLoadingPolicy.cs
// created by Alexander Shapoval
// e-mail: a.shapoval@twinwingames.com
// Copyright 2017 Luck Genome

using System;
using System.IO;
using LGPlatform.Log;
using UnityEngine;

namespace LGPlatform.AssetManagement
{
    /// <summary>
    /// Class that handles loading from local storage behaviour of AssetBundle manager.
    /// </summary>
    public class LocalBundleLoadingPolicy : IBundleLoadingPolicy 
    {
        /// <summary>
        /// Loading asset bundle from local storage.
        /// </summary>
        /// <param name="url">Path to asset bundle in streamingAssetsPath + AssetBundles path.</param>
        /// <param name="callback">Result callback.</param>
        public void Load(string url, Action<AssetBundleRef, Boolean> callback = null)
        {
            String path = Path.Combine(Application.streamingAssetsPath, $"AssetBundles/{url}");
            AssetBundle bundle = AssetBundle.LoadFromFile(path);

            if (bundle == null)
            {
                Logging.Log(Logging.Level.Error, Logging.Channel.Core, $"Can't load asset bundle from path: {path}");

                callback?.Invoke(null, false);

                return;
            }

            callback?.Invoke(new AssetBundleRef(1, bundle), true);
        }

        /// <summary>
        /// Unloading bundle from memory.
        /// </summary>
        /// <param name="bundleRef">Bundle wrapper</param>
        public void Unload(AssetBundleRef bundleRef)
        {
            bundleRef?.AssetBundle.Unload(true);
        }
    }
}
