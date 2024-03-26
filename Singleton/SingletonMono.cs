using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ {
    public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        private static T instance;
        public static T In {
            get {
                if ( instance == null ) {
                    instance = FindObjectOfType<T>();

    #if UNITY_EDITOR
                    if ( FindObjectsOfType( typeof( T ) ).Length > 1 ) {
                        Debug.LogError( "[SingletonMono] There should never be more than 1 singleton! Reopen the scene." );
                        return null;
                    }
    #endif
                    if ( instance == null ) {
                        GameObject go = new GameObject( typeof( T ).ToString() );
                        instance = go.AddComponent<T>();
                        DontDestroyOnLoad( go );
                    }
                }
                return instance;
            }
        }
        
        protected virtual void Awake()
        {
            if ( instance == null ) {
                instance = (T)this;
                DontDestroyOnLoad( gameObject );
            } else if ( instance != this ) {
                GameObject.Destroy( gameObject );
                return;
            }
            OnAwake();
        }
        protected virtual void OnAwake()
        {
            
        }
    }
}

