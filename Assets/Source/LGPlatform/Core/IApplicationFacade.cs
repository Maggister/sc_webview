// IApplicationFacade.cs
// created by Yaroslav Nevmerzhytskiy
// e-mail: y.nevmerzhytskiy@twinwingames.com
// Copyright 2017 Luck Genome

using LGPlatform.AssetManagement;

namespace LGPlatform.Core
{
    internal interface IApplicationFacade
    {
        /// <summary>
        /// Getter of the current Asset manager
        /// </summary>
        AssetManager<RemoteBundleLoadingPolicy> AssetManager
        {
            get;
        }
    }
}