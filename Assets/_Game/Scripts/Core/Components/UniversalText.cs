using Core.Extensions;
using TMPro;
using UnityEngine.UI;

namespace Core.Components
{
    public class UniversalText : MyMonoBehaviour
    {
        private Text _textComponent;
        private TextMeshPro _textMeshProComponent;
        private TextMeshProUGUI _textMeshProUguiComponent;

        public string text {
            get => GetText();
            set => SetText(value);
        }

        public new bool enabled {
            get => base.enabled;
            set {
                base.enabled = value;
                SetEnabled(value);
            }
        }

        ///////////////////////////////////////////////////

        private void OnEnable()
            => SetEnabled(true);

        private void OnDisable()
            => SetEnabled(false);

        ///////////////////////////////////////////////////

        private void Initialize()
        {
            var result = false;

            result |= AttachSafe(ref _textComponent);
            result |= AttachSafe(ref _textMeshProComponent);
            result |= AttachSafe(ref _textMeshProUguiComponent);

            if (!result)
                Logging.Error(_source, "There aren't any attached text component");
        }

        private void SetEnabled(bool status)
        {
            Initialize();

            if (_textComponent)
                _textComponent.enabled = status;
            if (_textMeshProComponent)
                _textMeshProComponent.enabled = status;
            if (_textMeshProUguiComponent)
                _textMeshProUguiComponent.enabled = status;
        }

        ///////////////////////////////////////////////////

        public void SetText(string text)
        {
            Initialize();

            if (_textComponent)
                _textComponent.text = text;
            if (_textMeshProComponent)
                _textMeshProComponent.text = text;
            if (_textMeshProUguiComponent)
                _textMeshProUguiComponent.text = text;
        }

        public string GetText()
        {
            Initialize();

            if (_textComponent)
                return _textComponent.text;
            if (_textMeshProComponent)
                return _textMeshProComponent.text;
            if (_textMeshProUguiComponent)
                return _textMeshProUguiComponent.text;

            return "";
        }
    }
}
