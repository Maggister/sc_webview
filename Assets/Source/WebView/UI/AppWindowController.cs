// AppWindowController.cs
// created by Valeriy Shcherba
// e-mail: v.shcherba@twinwingames.com
// Copyright 2017 Luck Genome

using LGPlatform.Core;
using UnityEditor;
using UnityEngine;

namespace WebView.UI
{
	public class AppWindowController : UnityBehaviour
	{
		[SerializeField] private AppWindowView _appView;
		
		private WebViewController m_webController;
		private GameObject m_bundleUIObject;
		
		protected override void OnAwake()
		{
			base.OnAwake();
			
			m_webController = new WebViewController();
			m_webController.Init();
		}

		protected override void OnStart()
		{
			base.OnStart();

//			_appView.LoadButton.onClick.AddListener(OnLoadButtonClicked);	
			_appView.SaveButton.onClick.AddListener(OnSaveButtonClicked);

			#if UNITY_EDITOR
			LoadBundleByUrl("https://www.dropbox.com/s/sj3kigf7n2sojzd/popup.test?dl=1");
			#endif
		}

		protected override void OnRelease()
		{
			base.OnRelease();

			_appView.SaveButton.onClick.RemoveAllListeners();
		}


		public void LoadBundleByUrl(string url)
		{
			if (string.IsNullOrEmpty(url)) return;
			
			Destroy(_appView.LinkInputField.gameObject);
			
			m_webController.AssetManager.LoadAssetBundle(url, (bundleRef, isSuccess) =>
			{
				if (!isSuccess) return;
				
				Debug.Log("LoadBundleByUrl 1");
				m_bundleUIObject = Instantiate(bundleRef.AssetBundle.LoadAsset("Popup - test")) as GameObject;
				Debug.Log("LoadBundleByUrl 2");
				m_bundleUIObject.transform.SetParent(GameObject.Find("UI").transform, false);
			});
		}
		
		private void OnSaveButtonClicked()
		{
			Debug.Log("OnSaveButtonClicked");
			BuildPipeline.BuildAssetBundle();
		}
	}
}