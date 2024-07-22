using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DatLycan.Packages.Utils {
    [CustomPropertyDrawer(typeof(SerializableType))]
    public class SerializableTypeDrawer : PropertyDrawer {
        private TypeFilterAttribute typeFilter;
        private string[] typeNames, typeFullNames;

        private void Initialize() {
            if (typeFullNames != null) return;
            
            typeFilter = (TypeFilterAttribute) Attribute.GetCustomAttribute(fieldInfo, typeof(TypeFilterAttribute));

            Type[] filteredTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => typeFilter == null ? DefaultFilter(t) : typeFilter.Filter(t))
                .ToArray();

            typeNames = filteredTypes.Select(t => t.ReflectedType == null ? t.Name : $"t.ReflectedType.Name + t.Name").ToArray();
            typeFullNames = filteredTypes.Select(t => t.AssemblyQualifiedName).ToArray();
        }

        private static bool DefaultFilter(Type type) => !type.IsAbstract && !type.IsInterface && !type.IsGenericType;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            Initialize();
            SerializedProperty typeIdProperty = property.FindPropertyRelative("assemblyQualifiedName");

            if (string.IsNullOrEmpty(typeIdProperty.stringValue)) {
                typeIdProperty.stringValue = typeFullNames.First();
                property.serializedObject.ApplyModifiedProperties();
            }

            int currentIndex = Array.IndexOf(typeFullNames, typeIdProperty.stringValue);
            int selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, typeNames);

            if (selectedIndex >= 0 && selectedIndex != currentIndex) {
                typeIdProperty.stringValue = typeFullNames[selectedIndex];
                property.serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
