// AppWindowController.cs
// created by Valeriy Shcherba
// e-mail: v.shcherba@twinwingames.com
// Copyright 2017 Luck Genome

using System.Collections.Generic;
using System.IO;
 
using LGPlatform.Core;
using LGPlatform.Core;
using UnityEngine;

namespace WebView.UI
{
	public class AppWindowController : UnityBehaviour
	{
		[SerializeField] private AppWindowView _appView;
		
		private WebViewController m_webController;

		private readonly List<string> m_consoleArgsList = new List<string>() {"-bundle_link"};
		
		private Dictionary<string, string> m_consoleCommandArgs = new Dictionary<string, string>();
		
		protected override void OnStart()
		{
			base.OnStart();

			m_webController = WebViewController.Instance;
			
			_appView.LoadButton.onClick.AddListener(OnLoadButtonClicked);	
			_appView.SaveButton.onClick.AddListener(OnSaveButtonClicked);

			for(int index = 0; index < System.Environment.GetCommandLineArgs().Length; index++)
			{
				if(m_consoleArgsList.Exists(x => x == System.Environment.GetCommandLineArgs()[index]))
				{
					m_consoleCommandArgs.Add(System.Environment.GetCommandLineArgs()[index], System.Environment.GetCommandLineArgs()[index + 1]);
				}
			}
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
			
			Application.OpenURL(m_consoleCommandArgs["-bundle_link"]);
			return;
//			if (string.IsNullOrEmpty(url)) return;

			m_webController.AssetManager.LoadAssetBundle("https://www.dropbox.com/s/sj3kigf7n2sojzd/popup.test?dl=1", (bundleRef, isSuccess) =>
			{
				Debug.Log(isSuccess);
				Debug.Log(bundleRef.AssetBundle == null);
				Debug.Log(bundleRef.AssetBundle.name);
				
//				AssetBundle/**/
				GameObject obj = Instantiate(bundleRef.AssetBundle.LoadAsset("Popup - test")) as GameObject;

//				Object bun = bundleRef.AssetBundle.LoadAsset("sas");
//				GameObject obj = Instantiate(bun) as GameObject;
			});
			
			Debug.Log("OnLoadButtonClicked");
		}
		
		private void OnSaveButtonClicked()
		{
			Debug.Log("OnSaveButtonClicked");
		}
	}
}