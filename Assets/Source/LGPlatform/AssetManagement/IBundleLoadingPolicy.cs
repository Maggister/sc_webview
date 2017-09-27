// IBundleLoadingPolicy.cs
// created by Alexander Shapoval
// e-mail: a.shapoval@twinwingames.com
// Copyright 2017 Luck Genome

using System;

namespace LGPlatform.AssetManagement
{
    /// <summary>
    /// Loading bundles policy interface, which define common methods for loading/unloading bundles
    /// </summary>
    public interface IBundleLoadingPolicy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        void Load(string url, Action<AssetBundleRef, Boolean> callback = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bundleRef"></param>
        void Unload(AssetBundleRef bundleRef);
    }
}