// AppWindowController.cs
// created by Valeriy Shcherba
// e-mail: v.shcherba@twinwingames.com
// Copyright 2017 Luck Genome

using LGPlatform.AssetManagement;
using LGPlatform.Core;
using UnityEngine;

namespace WebView.UI
{
	public class AppWindowController : UnityBehaviour
	{
		[SerializeField] private AppWindowView _appView;
		
		private WebViewController m_webController;
		
		protected override void OnStart()
		{
			base.OnStart();

			m_webController = WebViewController.Instance;
			
			_appView.LoadButton.onClick.AddListener(OnLoadButtonClicked);	
			_appView.SaveButton.onClick.AddListener(OnSaveButtonClicked);
		}

		protected override void OnRelease()
		{
			base.OnRelease();
			
			_appView.LoadButton.onClick.RemoveAllListeners();
			_appView.SaveButton.onClick.RemoveAllListeners();
		}


		private void OnLoadButtonClicked()
		{
			string url = _appView.LinkInputField.text;
			
			if (string.IsNullOrEmpty(url)) return;

			m_webController.AssetManager.LoadAssetBundle(url, (bundleRef, isSuccess) =>
			{
				Debug.Log(isSuccess);
			});
			
			Debug.Log("OnLoadButtonClicked");
		}
		
		private void OnSaveButtonClicked()
		{
			Debug.Log("OnSaveButtonClicked");
		}
	}
}