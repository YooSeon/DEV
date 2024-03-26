using System;
using UnityEngine;

namespace SJ
{
    public abstract class MonoSingletonWithCreateDontDestory<T> : MonoBehaviour where  T: MonoSingletonWithCreateDontDestory<T>
    {
        protected static T instance = null;
        public static T In
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(T)) as T;
                    if (instance == null)
                    {
                        instance = new GameObject("Singleton of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    }
                }
                return instance;
            }
        }
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }

            if (instance != null)
            {
                DontDestroyOnLoad(instance.gameObject);
            }
        }

        protected void OnDestroy()
        {
            instance = null;
        }
    }
}

