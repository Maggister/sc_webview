// Singleton.cs
// created by Yaroslav Nevmerzhytskiy
// e-mail: y.nevmerzhytskiy@twinwingames.com
// Copyright 2017 Luck Genome

namespace LGPlatform.Core
{
    /// <summary>
    /// Singleton pattern implementation
    /// </summary>
    /// <typeparam name="T">CRT pattern, to allow generic instance</typeparam>
    internal class Singleton<T>
        where T : Singleton<T>, new()
    {
        /// <summary>
        /// Returns the instance of the singleton
        /// </summary>
        public static T Instance
        {
            get
            {
                return m_instance ?? (m_instance = new T());
            }
        }

        private static T m_instance;

        protected Singleton()
        {
        }
    }
}