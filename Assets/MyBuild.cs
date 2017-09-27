// MyBuild.cs
// created by Valeriy Shcherba
// e-mail: v.shcherba@twinwingames.com
// Copyright 2017 Luck Genome

using UnityEngine;
using WebView;

namespace LGPlatform
{
	public static class MyBuild 
	{
		public static void Test()
		{
			MonoBehaviour.Destroy(GameObject.Find("UI"));
		}
	}
}