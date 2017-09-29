// AppWindowController.cs
// created by Valeriy Shcherba
// e-mail: v.shcherba@twinwingames.com
// Copyright 2017 Luck Genome

using LGPlatform.Core;
using LGPlatform.Log;
using UnityEngine;

namespace WebView.UI
{
	/// <summary>
	/// Controller for app window.
	/// </summary>
	public class AppWindowController : UnityBehaviour
	{
		private AppController m_webController;
		private GameObject m_bundleUIObject;

		protected override void OnAwake()
		{
			base.OnAwake();
			
			m_webController = new AppController();
			m_webController.Init();
		}

		/// <summary>
		/// Downloadin bundle and create object from asset bundle.
		/// How call from JS: gameInstance.SendMessage("Window - App","DownloadBundleByUrl", "{Link_To_Bundle}");
		/// </summary>
		/// <param name="url"> Url from asset bundle location.</param>
		public void DownloadBundleByUrl(string url)
		{
			if (string.IsNullOrEmpty(url)) return;

			m_webController.AssetManager.LoadAssetBundle(url, (bundleRef, isSuccess) =>
			{
				if (!isSuccess) return;
				
				if (m_bundleUIObject != null) Destroy(m_bundleUIObject.gameObject);

				Object[] loadedAssetsFromBundle = bundleRef.AssetBundle.LoadAllAssets();

				if(loadedAssetsFromBundle.Length <= 0)
				{
					Logging.Log(Logging.Level.Error, Logging.Channel.Game, "Current bundle empty.");
					return;
				}
				
				m_bundleUIObject = Instantiate(loadedAssetsFromBundle[0]) as GameObject;
				m_bundleUIObject.transform.SetParent(transform, false);
			});
		}
	}
}