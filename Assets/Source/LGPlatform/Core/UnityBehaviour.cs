// UnityBehaviour.cs
// created by Yaroslav Nevmerzhytskiy
// e-mail: y.nevmerzhytskiy@twinwingames.com
// Copyright 2017 Luck Genome

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// TODO: Attribute is added for testing purposes. Unit tests can access internal members from Assembly-CSharp-Editor assembly.
[assembly: InternalsVisibleTo("Assembly-CSharp-Editor", AllInternalsVisible = true)]

namespace LGPlatform.Core
{
    /// <summary>
    /// Wrapper over Unity's MonoBehaviour, that extends MB's functionality
    /// </summary>
    public class UnityBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Cached Transform component, created due to expensive call of the 'transform' property.
        /// </summary>
        public Transform Transform
        {
            get
            {
                return m_transform;
            }
        }

        /// <summary>
        /// Cached RectTransform component, created due to avoid redundant cast of the 'transform' to RectTransform. Can be null on non-UI objects.
        /// </summary>
        public RectTransform RectTransform
        {
            get
            {
                return m_rectTransform;
            }
        }

        private Transform m_transform;
        private RectTransform m_rectTransform;
        private Single m_timer;
        private Single m_delay;
        private Boolean m_isLoop;
        private Boolean m_isDelayed;

        internal UnityBehaviour()
        {
        }

        /// <summary>
        /// Allows to delay invocation of the method. <b>The method will be executed at least once.</b>
        /// Invocation can be looped. <b>Delay is frame-length-dependant</b>
        /// </summary>
        /// <param name="delay">desired delay</param>
        /// <param name="isLoop">marks if method should be looped</param>
        protected void DelayUpdate(Single delay, Boolean isLoop = true)
        {
            m_timer = 0f;
            m_delay = delay;
            m_isDelayed = true;
            m_isLoop = isLoop;
        }

        /// <summary>
        /// Stops execution of the delayed update
        /// </summary>
        protected void StopDelayedUpdate()
        {
            m_timer = 0f;
            m_delay = 0f;
            m_isDelayed = false;
            m_isLoop = false;
        }

        /// <summary>
        /// Wrapper on Unity's Awake() method
        /// </summary>
        protected virtual void OnAwake()
        {
        }

        /// <summary>
        /// Wrapper on Unity's Start() method
        /// </summary>
        protected virtual void OnStart()
        {
        }

        /// <summary>
        /// Wrapper on Unity's OnDestroy() method
        /// </summary>
        protected virtual void OnRelease()
        {
        }

        /// <summary>
        /// Wrapper on Unity's Update() method
        /// </summary>
        protected virtual void OnUpdate()
        {
        }

        /// <summary>
        /// Wrapper on Unity's FixedUpdate() method
        /// </summary>
        protected virtual void OnFixedUpdate()
        {
        }

        /// <summary>
        /// Wrapper on Unity's LateUpdate() method
        /// </summary>
        protected virtual void OnLateUpdate()
        {
        }

        /// <summary>
        /// Is called every [delay] seconds.
        /// </summary>
        protected virtual void OnDelayedUpdate()
        {
        }

        /// <summary>
        /// Set object visible.
        /// <param name="isActive">Is it active?</param>
        /// </summary>
        public virtual void SetActive(Boolean isActive)
        {
            gameObject.SetActive(isActive);
        }

        private void Awake()
        {
            m_transform = transform;
            
            if(m_transform is RectTransform)
                m_rectTransform = (RectTransform)m_transform;
            
            OnAwake();
        }

        private void Start()
        {
            OnStart();
        }

        private void OnDestroy()
        {
            OnRelease();
        }

        private void Update()
        {
            OnUpdate();
            if(m_isDelayed)
            {
                if(m_timer >= m_delay)
                {
                    OnDelayedUpdate();

                    m_timer = 0f;
                    if(!m_isLoop)
                    {
                        m_isDelayed = false;
                    }
                }

                m_timer += Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            OnFixedUpdate();
        }

        private void LateUpdate()
        {
            OnLateUpdate();
        }
    }
}