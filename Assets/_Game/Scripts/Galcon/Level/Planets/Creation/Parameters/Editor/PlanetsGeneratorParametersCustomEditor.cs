using UnityEditor;
using UnityEngine;

namespace Galcon.Level.Planets.Creation.Parameters.Editor
{
    [CustomEditor(typeof(PlanetsGeneratorParameters))]
    class PlanetsGeneratorParametersCustomEditor : UnityEditor.Editor
    {
        private SerializedObject _this;
        private SerializedProperty _spritesProperty;

        private const string _PROPERTY_NAME = "_PossibleSprites";
        private const string _PROPERTY_VISIBLE_NAME = "Possible Sprites";
        private const string _SUNPROPERTY_NAME = "Sprite";

        ////////////////////////////////////////////////

        private void OnEnable()
        {
            _this = new SerializedObject(target);
            _spritesProperty = _this.FindProperty(_PROPERTY_NAME);
        }

        ////////////////////////////////////////////////

        public override void OnInspectorGUI()
        {
            _this.Update();

            DrawDefaultInspector();

            EditorGUILayout.Space(20);
            DrawArraySizeProperty(ref _spritesProperty);
            EditorGUILayout.Space(10);

            EditorGUILayout.BeginVertical();
            DrawArrayProperty(ref _spritesProperty);
            EditorGUILayout.EndVertical();

            _this.ApplyModifiedProperties();
        }

        ////////////////////////////////////////////////

        private void DrawArraySizeProperty(ref SerializedProperty arrayProperty)
            => arrayProperty.arraySize = EditorGUILayout.IntField(_PROPERTY_VISIBLE_NAME, arrayProperty.arraySize);

        private void DrawArrayProperty(ref SerializedProperty arrayProperty)
        {
            for (int i = 0; i < arrayProperty.arraySize; ++i)
                DrawArraySubProperty(arrayProperty, i);
        }

        private void DrawArraySubProperty(SerializedProperty arrayProperty, int index)
        {
            var sunProperty = arrayProperty.GetArrayElementAtIndex(index);
            DrawSpriteProperty(ref sunProperty, index);
        }

        private void DrawSpriteProperty(ref SerializedProperty property, int index)
            => property.objectReferenceValue = (Sprite)EditorGUILayout.ObjectField(
                label: $"{_SUNPROPERTY_NAME} {index + 1}", 
                obj: property.objectReferenceValue, 
                objType: typeof(Sprite), 
                allowSceneObjects: false);
    }
}
