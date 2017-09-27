// AppWindowView.cs
// created by Valeriy Shcherba
// e-mail: v.shcherba@twinwingames.com
// Copyright 2017 Luck Genome

using LGPlatform.Core;
using UnityEngine;
using UnityEngine.UI;

namespace WebView.UI
{
	public class AppWindowView : UnityBehaviour
	{
		[SerializeField] private InputField _linkInputField;
		[SerializeField] private Button _loadButton;
		[SerializeField] private Button _saveButton;

		public InputField LinkInputField => _linkInputField;
		public Button LoadButton => _loadButton;
		public Button SaveButton => _saveButton;
	}
}