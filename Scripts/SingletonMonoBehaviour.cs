using System;
using UnityEngine;

namespace Tiutilities
{

    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {

        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Type t = typeof(T);

                    instance = (T)FindObjectOfType(t);
                    if (instance == null)
                    {
                        Debug.LogError("There's no GameObject attaching " + t);
                    }
                }

                return instance;
            }
        }

        [SerializeField] private bool m_DontDestroyOnLoad = true;

        virtual protected void Awake()
        {
            // 他のGameObjectにアタッチされているか調べる.
            // アタッチされている場合は破棄する.
            if (this != Instance)
            {
                Destroy(this);
                //Destroy(this.gameObject);
                Debug.LogError(
                    typeof(T) +
                    " is already attached to the other GameObject, so destroyed GameObject." +
                    " The one attaching is " + Instance.gameObject.name);
                return;
            }

            if (m_DontDestroyOnLoad) DontDestroyOnLoad(this.gameObject);
        }

    }
}