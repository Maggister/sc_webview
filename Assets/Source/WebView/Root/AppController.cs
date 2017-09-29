// WebViewContext.cs
// created by Valeriy Shcherba
// e-mail: v.shcherba@twinwingames.com
// Copyright 2017 Luck Genome

using LGPlatform.AssetManagement;
using LGPlatform.Core;

namespace WebView
{
	internal sealed class AppController : Singleton<AppController>
	{
		private AssetManager<RemoteBundleLoadingPolicy> m_assetManager;

		public AssetManager<RemoteBundleLoadingPolicy> AssetManager => m_assetManager;

		public void Init()
		{
			m_assetManager = new AssetManager<RemoteBundleLoadingPolicy>();
		}
	}
}