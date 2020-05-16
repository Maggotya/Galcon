using UnityEditor;
using UnityEngine;

namespace Core.Structs.Editor
{
    [CustomPropertyDrawer(typeof(Range))]
    class RangeDrawer : PropertyDrawer
    {
        private SerializedProperty _Min { get; set; }
        private SerializedProperty _Max { get; set; }

        private string _name_Cache { get; set; }
        private bool _isCached { get; set; }

        private const float _MIN_SCREEN_WIDTH = 300f;
        private const float _LINE_HEIGHT = 20f;
        private const float _MAX_HEIGHT = 20f;

        ///////////////////////////////////////////////////

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Cache(property);

            var contentPosition = GetContentPosition(position);
            EditorGUIUtility.labelWidth = 30f;

            DrawProperty(contentPosition, label, _Min);

            contentPosition.x += contentPosition.width * 1.1f;

            DrawProperty(contentPosition, label, _Max);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => Screen.width < _MIN_SCREEN_WIDTH ? ( _MAX_HEIGHT + _LINE_HEIGHT ) : _LINE_HEIGHT;

        #region CACHING
        private void Cache(SerializedProperty property)
        {
            if (_isCached)
                return;

            _name_Cache = property.displayName;

            property.Next(true);
            _Min = property.Copy();

            property.Next(true);
            _Max = property.Copy();

            _isCached = true;
        }
        #endregion // CACHING

        #region CONTENT_POSITION
        private Rect GetContentPosition(Rect position)
        {
            var contentPosition = EditorGUI.PrefixLabel(position, new GUIContent(_name_Cache));

            contentPosition.width *= 0.5f;
            contentPosition.x -= contentPosition.width * 0.3f;

            IncreaseIndentLevelIfNecessary(ref contentPosition, position);

            return contentPosition;
        }

        private void IncreaseIndentLevelIfNecessary(ref Rect contentPosition, Rect position)
        {
            if (position.height <= _MAX_HEIGHT)
                return;

            position.height = _MAX_HEIGHT;
            EditorGUI.indentLevel++;

            contentPosition = EditorGUI.IndentedRect(position);
            contentPosition.width *= 0.45f;
            contentPosition.y += _LINE_HEIGHT;
        }
        #endregion // CONTENT_POSITION

        #region DRAWING_PROPERTY
        private void DrawProperty(Rect contentPosition, GUIContent label, SerializedProperty property)
        {
            EditorGUI.BeginProperty(contentPosition, label, property);
            EditorGUI.BeginChangeCheck();

            var content = new GUIContent(PropertyName(property));
            var newVal = EditorGUI.FloatField(contentPosition, content, property.floatValue);

            if (EditorGUI.EndChangeCheck())
                property.floatValue = newVal;

            EditorGUI.EndProperty();
        }

        private string PropertyName(SerializedProperty property)
            => property.name.Replace("_", "");
        #endregion // DRAWING_PROPERTY
    }
}
