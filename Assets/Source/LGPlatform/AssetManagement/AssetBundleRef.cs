// AssetBundleRef.cs
// created by Alexander Shapoval
// e-mail: a.shapoval@twinwingames.com
// Copyright 2017 Luck Genome

using System;
using UnityEngine;

namespace LGPlatform.AssetManagement
{
    public sealed class AssetBundleRef
    {
        public readonly AssetBundle AssetBundle;
        public readonly Int32 Version;

        public AssetBundleRef(Int32 version, AssetBundle assetBundle)
        {
            this.Version = version;
            this.AssetBundle = assetBundle;
        }
    }   
}
