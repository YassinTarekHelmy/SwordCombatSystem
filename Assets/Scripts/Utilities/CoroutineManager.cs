using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities {
    public class CoroutineManager : MonoBehaviour
    {
        public static CoroutineManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public Coroutine StartRemoteCoroutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }

        public void StopRemoteCoroutine(Coroutine routine)
        {
            StopCoroutine(routine);
        }

        public void StopAllRemoteCoroutines()
        {
            StopAllCoroutines();
        }

    }
}