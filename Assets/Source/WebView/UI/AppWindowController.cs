// AppWindowController.cs
// created by Valeriy Shcherba
// e-mail: v.shcherba@twinwingames.com
// Copyright 2017 Luck Genome

using LGPlatform.Core;
using UnityEngine;

namespace WebView.UI
{
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

		protected override void OnStart()
		{
			base.OnStart();

//			#if UNITY_EDITOR
//			DownloadBundleByUrl("[Popup - test, https://www.dropbox.com/s/sj3kigf7n2sojzd/popup.test?dl=1]");  
//			#endif
		}

		//
		/// <summary>
		/// Download bundle and create object from asset bundle.
		/// How call from JS: gameInstance.SendMessage("{Object_Name}","DownloadBundleByUrl", "{Link_To_Bundle}");
		/// </summary>
		/// <param name="objectName"> Name of object that need to load from asset bundle.</param>
		/// <param name="url"> Link for downloading bundle.</param>
		public void DownloadBundleByUrl(string jsonConfig)
		{
			Debug.Log($"{jsonConfig}");
			Config config = JsonUtility.FromJson<Config>(jsonConfig);

			
			
			if (string.IsNullOrEmpty(config.BundleUrl)) return;

			m_webController.AssetManager.LoadAssetBundle(config.BundleUrl, (bundleRef, isSuccess) =>
			{
				if (!isSuccess) return;
				
				if (m_bundleUIObject != null) Destroy(m_bundleUIObject.gameObject);

				m_bundleUIObject = Instantiate(bundleRef.AssetBundle.LoadAsset(config.ObjectName)) as GameObject;
				m_bundleUIObject.transform.SetParent(transform, false);
			});
		}
		
		[System.Serializable]
		private class Config
		{
			public string ObjectName;
			public string BundleUrl;
		}
	}
}