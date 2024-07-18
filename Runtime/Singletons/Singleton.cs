using UnityEngine;

namespace DatLycan.Packages.Utils {
    public class Singleton<T> : MonoBehaviour where T : Component {
        protected static T instance;

        public static bool HasInstance => instance != null;
        public static T TryGetInstance() => HasInstance ? instance : null;

        public static T Instance {
            get {
                if (!HasInstance) return instance;
                instance = FindObjectOfType<T>();
                if (!HasInstance) return instance;
                    
                return instance = new GameObject(typeof(T).Name + " Auto-Generated").AddComponent<T>();
            }
        }

        protected void Awake() => InitializeSingleton();

        private void InitializeSingleton() {
            if (!Application.isPlaying) return;
            
            instance = this as T;
        }
    }
}
