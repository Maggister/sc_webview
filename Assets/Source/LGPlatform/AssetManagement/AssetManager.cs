// AssetManager.cs
// created by Yaroslav Nevmerzhytskiy
// e-mail: y.nevmerzhytskiy@twinwingames.com
// Copyright 2017 Luck Genome

using System;
using System.Collections.Generic;
using LGPlatform.Log;

namespace LGPlatform.AssetManagement
{
    /// <summary>
    /// The class, that handles resources, loaded from the remote path. 
    /// E.g. AssetBundles, profile pictures from Facebook etc.
    /// </summary>
    internal class AssetManager<TBundleLoadingPolicy> where TBundleLoadingPolicy : IBundleLoadingPolicy, new()
    {
        private readonly Dictionary<String, AssetBundleRef> m_assetBundles = new Dictionary<string, AssetBundleRef>();

        /// <summary>
        /// Incapsulation for loading asset bundle from remote path.
        /// </summary>
        /// <param name="url">Url to download bundle</param>
        /// <param name="callback">Success of Failed callback on finish loading</param>
        public void LoadAssetBundle(String url, Action<AssetBundleRef, Boolean> callback = null)
        {
            TBundleLoadingPolicy policy = new TBundleLoadingPolicy();
            policy.Load(url, (bundle, success) =>
            {
                if (success)
                {
                    m_assetBundles.Add(bundle.AssetBundle.name, bundle);
                    Logging.Log(Logging.Level.Message, Logging.Channel.Core, "Loaded bundle at url: " + url);
                }

                callback?.Invoke(bundle, success);
            });
        }

        /// <summary>
        /// Method, which should be used to get specific asset from specific asset bundle.
        /// </summary>
        /// <param name="assetName">Name of needed asset</param>
        /// <param name="bundleName">Name of bundle, which asset belongs</param>
        /// <typeparam name="TAsset">Type of asset</typeparam>
        /// <returns>Asset or null in case of error</returns>
        public TAsset GetAsset<TAsset>(String assetName, String bundleName) where TAsset : UnityEngine.Object 
        {
            AssetBundleRef bundleRef;
            if (!m_assetBundles.TryGetValue(bundleName, out bundleRef))
            {
                Logging.Log(Logging.Level.Error, Logging.Channel.Core, $"Can't find [{bundleName}] bundle in cached asset bundles.");
                return null;
            }

            TAsset asset = bundleRef.AssetBundle.LoadAsset<TAsset>(assetName);
            if (asset == null)
            {
                Logging.Log(Logging.Level.Error, Logging.Channel.Core, $"Can't find [{assetName}] asset in asset bundle [{bundleName}].");
                return null;
            }

            return asset;
        }
        
        /// <summary>
        /// Method, which should be used to get assets with specific type from asset bundle.
        /// </summary>
        /// <param name="bundleName">Name of bundle, which assets belongs</param>
        /// <typeparam name="TAsset">Type of needed assets</typeparam>
        /// <returns>Array of assets with given type or null in case of error</returns>
        public TAsset[] GetAssets<TAsset>(String bundleName) where TAsset : UnityEngine.Object 
        {
            AssetBundleRef bundleRef;
            if (!m_assetBundles.TryGetValue(bundleName, out bundleRef))
            {
                Logging.Log(Logging.Level.Error, Logging.Channel.Core, $"Can't find [{bundleName}] bundle in cached asset bundles.");
                return null;
            }

            TAsset[] assets = bundleRef.AssetBundle.LoadAllAssets<TAsset>(); //bundleRef.AssetBundle.LoadAsset<TAsset>(assetName);
            if (assets == null || assets.Length == 0)
            {
                Logging.Log(Logging.Level.Error, Logging.Channel.Core, $"Can't find any [{typeof(TAsset).Name}] assets in asset bundle [{bundleName}].");
                return null;
            }

            return assets;
        }

        /// <summary>
        /// Unload bundle from memory if loaded.
        /// </summary>
        /// <param name="bundleName">Name of bundle, which should be unloaded</param>
        public void Unload(String bundleName)
        {
            if (!m_assetBundles.ContainsKey(bundleName))
            {
                Logging.Log(Logging.Level.Warning, Logging.Channel.Core, $"Can't unload {bundleName}: not present in dictionary");
                return;
            }
                
            TBundleLoadingPolicy policy = new TBundleLoadingPolicy();
            policy.Unload(m_assetBundles[bundleName]);

            Logging.Log(Logging.Level.Message, Logging.Channel.Core, $"Unloading bundle: {bundleName}");
            
            m_assetBundles.Remove(bundleName);
        }

        /// <summary>
        /// Unload all bundles from memory.
        /// </summary>
        public void UnloadAll()
        {
            foreach (KeyValuePair<String, AssetBundleRef> kvp in m_assetBundles)
            {
                kvp.Value.AssetBundle.Unload(true);
            }
            
            Logging.Log(Logging.Level.Message, Logging.Channel.Core, "Unloading all asset bundles...");
            
            m_assetBundles.Clear();
        }
    }
}