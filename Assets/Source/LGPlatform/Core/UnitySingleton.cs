// UnitySingleton.cs
// created by Yaroslav Nevmerzhytskiy
// e-mail: y.nevmerzhytskiy@twinwingames.com
// Copyright 2017 Luck Genome

using UnityEngine;

namespace LGPlatform.Core
{
    /// <summary>
    /// Singleton, derived from MonoBehaviour with associated GameObject
    /// </summary>
    /// <typeparam name="T">CRT pattern, to allow generic instance</typeparam>
    internal class UnitySingleton<T> : UnityBehaviour
        where T : UnitySingleton<T>
    {
        /// <summary>
        /// Returns the instance of the singleton
        /// </summary>
        public static T Instance
        {
            get
            {
                if(m_instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    DontDestroyOnLoad(go);
                    m_instance = go.AddComponent<T>();
                }

                return m_instance;
            }
        }

        private static T m_instance;

        protected UnitySingleton()
        {
        }
    }
}