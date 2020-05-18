using Core;
using Core.Components;
using Core.Structs;
using UnityEngine;

namespace Galcon.Level.Population.View
{
    public class PopulationView : ComponentView, IPopulationView
    {
        private readonly UniversalText _textComponent;

        ////////////////////////////////////////////

        public PopulationView(GameObject modelObject)
        {
            InitComponent(ref _textComponent, modelObject);
        }

        ////////////////////////////////////////////

        public void Hide()
        {
            _textComponent.enabled = false;
            Logging.Log(_source, "Hide");
        }

        public void Show()
        {
            _textComponent.enabled = true;
            Logging.Log(_source, "Show");
        }

        public void UpdateView(int population)
            => _textComponent.text = population.ToString();
    }
}
