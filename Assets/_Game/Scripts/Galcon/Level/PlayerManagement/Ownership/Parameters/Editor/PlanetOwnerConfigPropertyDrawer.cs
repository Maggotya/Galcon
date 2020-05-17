using UnityEditor;
using UnityEngine;

namespace Galcon.Level.PlayerManagement.Ownership.Parameters.Editor
{
    [CustomPropertyDrawer(typeof(PlanetOwnerConfig))]
    class PlanetOwnerConfigPropertyDrawer : PropertyDrawer
    {
        private const string _TAG_PROPERTY_NAME = "_OwnerTag";
        private const string _COLOR_PROPERTY_NAME = "_OwnerColor";

        private float _fieldWidth(Rect position) => position.width * 0.4f;
        private float _fieldHeight(Rect position) => position.height * 0.9f;
        private float _offset(Rect position) => position.width * 0.05f;

        //////////////////////////////////////////////////////////////

        #region OVERRIDES

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUIUtility.singleLineHeight * 2f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.indentLevel++;

            DrawTagField(position, property);
            DrawColorField(position, property);

            EditorGUI.indentLevel--;
            EditorGUI.EndProperty();
        }

        #endregion // OVERRIDES

        #region DRAWING

        private void DrawTagField(Rect position, SerializedProperty property)
        {
            var tagRect = GetTagRect(position);
            var tagProperty = GetTagProperty(property);
            var tagStyle = GetTagStyle();

            EditorGUI.BeginChangeCheck();

            var tag = EditorGUI.TextField(tagRect, tagProperty.stringValue, tagStyle);

            if (EditorGUI.EndChangeCheck())
                tagProperty.stringValue = tag;

        }

        private void DrawColorField(Rect position, SerializedProperty property)
        {
            var colorRect = GetColorRect(position);
            var colorProperty = GetColorProperty(property);

            EditorGUI.BeginChangeCheck();

            var col = EditorGUI.ColorField(colorRect, colorProperty.colorValue);

            if (EditorGUI.EndChangeCheck())
                colorProperty.colorValue = col;
        }

        #endregion // DRAWING

        #region GETTERS

        private SerializedProperty GetTagProperty(SerializedProperty property)
            => property.FindPropertyRelative(_TAG_PROPERTY_NAME);

        private SerializedProperty GetColorProperty(SerializedProperty property)
            => property.FindPropertyRelative(_COLOR_PROPERTY_NAME);

        private GUIStyle GetTagStyle()
        {
            var guiStyle = new GUIStyle(GUI.skin.GetStyle("textField"));
            guiStyle.alignment = TextAnchor.MiddleCenter;

            return guiStyle;
        }

        private Rect GetTagRect(Rect position)
            => new Rect(
                position.x,
                position.y,
                _fieldWidth(position),
                _fieldHeight(position));

        private Rect GetColorRect(Rect position)
            => new Rect(
                position.x + _fieldWidth(position) + _offset(position),
                position.y,
                _fieldWidth(position),
                _fieldHeight(position));

        #endregion // GETTERS

    }
}
