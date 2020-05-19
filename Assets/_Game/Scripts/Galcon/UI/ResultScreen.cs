using Core;
using Core.Components;
using Core.Extensions;

namespace Galcon.UI
{
    public class ResultScreen : MyMonoBehaviour
    {
        private UniversalText _universalText;

        public string status { get; private set; }

        /////////////////////////////////////////////////////

        private void OnEnable()
            => Attach(ref _universalText);

        /////////////////////////////////////////////////////

        public void SetStatus(string status)
        {
            this.status = status;
            UpdateText();
            Logging.Log(_source, $"Set status: '{status}'");
        }

        /////////////////////////////////////////////////////

        private void UpdateText()
            => _universalText?.SetText($"{status}!");
    }
}
