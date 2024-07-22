using System;
using UnityEngine;

namespace DatLycan.Packages.Utils {
    [Serializable]
    public class SerializableType : ISerializationCallbackReceiver {
        [SerializeField] private string assemblyQualifiedName = string.Empty;
        
        public Type Type { get; private set; }

        void ISerializationCallbackReceiver.OnBeforeSerialize() {
            assemblyQualifiedName = Type?.AssemblyQualifiedName ?? assemblyQualifiedName;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() {
            if (!TryGetType(assemblyQualifiedName, out Type type)) {
                Debug.LogError($"Type {assemblyQualifiedName} not found.");
                return;
            }

            Type = type;
        }

        private static bool TryGetType(string typeString, out Type type) {
            type = Type.GetType(typeString);
            return type != null || !string.IsNullOrEmpty(typeString);
        }
    }
}