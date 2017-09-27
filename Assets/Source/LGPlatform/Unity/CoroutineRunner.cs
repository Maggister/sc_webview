// CoroutineRunner.cs
// created by Alexander Shapoval
// e-mail: a.shapoval@twinwingames.com
// Copyright 2017 Luck Genome

using System;
using System.Collections;
using LGPlatform.Core;
using UnityEngine;

namespace LGPlatform.Unity
{
    /// <summary>
    /// Helper MonoBehaviour class for running coroutines in non-unity environment.
    /// </summary>
    internal class CoroutineRunner : UnitySingleton<CoroutineRunner>
    {
        /// <summary>
        /// Run IEnumerator as a coroutine.
        /// </summary>
        /// <param name="coroutine">Runned coroutine</param>
        /// <param name="callback">Callback action which will be called after coroutine's finish</param>
        /// <returns></returns>
        public static Coroutine Run(IEnumerator coroutine, Action callback = null)
        {
            return Instance.StartCoroutine(RunEnumerator(coroutine, callback));
        }

        /// <summary>
        /// Just wait some time and raise callback.
        /// </summary>
        /// <param name="seconds">Time in seconds</param>
        /// <param name="callback">Callback action</param>
        /// <returns>Coroutine instance</returns>
        public static Coroutine Wait(Single seconds, Action callback)
        {
            return Instance.StartCoroutine(WaitEnumerator(seconds, callback));
        }

        /// <summary>
        /// Run coroutine which will raise callback every frame for the defined period.
        /// </summary>
        /// <param name="totalTime">After this period in seconds coroutine will stop</param>
        /// <param name="callback">Callback action, which raises every frame. Will send float value clamped between 0 and 1, which represents passed percent.</param>
        /// <param name="onFinish">On finish coroutine callback</param>
        /// <returns>Coroutine instance</returns>
        public static Coroutine EveryFrame(Single totalTime, Action<Single> callback, Action onFinish = null)
        {
            return Instance.StartCoroutine(EveryFrameEnumerator(totalTime, callback, onFinish));
        }

        /// <summary>
        /// Run coroutine which will check condition and raise callback every frame until condition will be true.
        /// </summary>
        /// <param name="getCondition">Func to check condition.</param>
        /// <param name="callback">Callback action, which raises every frame till getCondition() will return true.</param>
        /// <returns></returns>
        public static Coroutine Till(Func<Boolean> getCondition, Action callback)
        {
            return Instance.StartCoroutine(TillEnumerator(getCondition, callback));
        }

        private static IEnumerator RunEnumerator(IEnumerator coroutine, Action callback = null)
        {
            yield return Instance.StartCoroutine(coroutine);

            callback?.Invoke();
        }
        
        private static IEnumerator WaitEnumerator(Single seconds, Action callback)
        {
            yield return new WaitForSeconds(seconds);

            callback?.Invoke();
        }

        private static IEnumerator EveryFrameEnumerator(Single totalTime, Action<Single> callback, Action onFinish = null)
        {
            Single time = totalTime;
            
            while (time >= 0)
            {
                yield return new WaitForEndOfFrame();

                time -= Time.smoothDeltaTime;

                callback( 1 - Mathf.Clamp01( time / totalTime ) );
            }

            onFinish?.Invoke();
        }
        
        private static IEnumerator TillEnumerator(Func<Boolean> getCondition, Action callback)
        {
            while (getCondition())
            {
                callback();

                yield return new WaitForEndOfFrame();
            }
        }
    }
}