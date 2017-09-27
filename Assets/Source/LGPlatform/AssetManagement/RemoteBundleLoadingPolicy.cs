// RemoteBundleLoadingPolicy.cs
// created by Dmitry Shkurko
// e-mail: d.shkurko@twinwingames.com
// Copyright 2017 Luck Genome

using System;
using System.Collections;
using System.IO;
using LGPlatform.Log;
using LGPlatform.Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace LGPlatform.AssetManagement
{
    /// <summary>
    /// Class that handles remote loading for AssetBundle manager.
    /// </summary>
    public class RemoteBundleLoadingPolicy : IBundleLoadingPolicy
    {
        /// <summary>
        /// Loading asset bundle from remote storage.
        /// </summary>
        /// <param name="url">Resource url in the remote storage.</param>
        /// <param name="callback">Result callback.</param>
        public void Load(string url, Action<AssetBundleRef, Boolean> callback = null)
        {
//            String path = Path.Combine(Application.streamingAssetsPath, $"AssetBundles/{url}");
            CoroutineRunner.Run(LoadRemote(url, callback));
        }

        /// <summary>
        /// Unloading bundle from memory.
        /// </summary>
        /// <param name="bundleRef">Bundle wrapper</param>
        public void Unload(AssetBundleRef bundleRef)
        {
            bundleRef?.AssetBundle.Unload(true);
        }

        private IEnumerator LoadRemote(String url, Action<AssetBundleRef, Boolean> callback = null)
        {
            UnityWebRequest request = UnityWebRequest.GetAssetBundle(url);

            yield return request.Send();

            AssetBundle bundle = null;

            if (request.isDone)
            {
                bundle = ((DownloadHandlerAssetBundle) request.downloadHandler).assetBundle;
            }

            if (bundle == null)
            {
                Logging.Log(Logging.Level.Error, Logging.Channel.Core, $"Can't load asset bundle from url: {url}");

                callback?.Invoke(null, false);
            }
            else
            {
                callback?.Invoke(new AssetBundleRef(1, bundle), true);
            }
        }
    }
}