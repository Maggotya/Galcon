using Core;
using Core.Extensions;
using Core.Handlers;
using Galcon.Level.Population.Model;
using Galcon.Level.Population.View;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Galcon.Level.Population
{
    class PopulationManager : MyMonoBehaviour, IPopulationManager
    {
        [SerializeField] private UnityEvent _OnPopulationExterminated;
        [SerializeField] private UnityEvent _OnEmergenceOfPopulation;
        [SerializeField] private IntUnityEvent _OnPopulationIncreased;

        private IPopulationModel _model;
        private IPopulationView _view;
        private ITimerHandler _timer;
        private int _populationPerInterval;
        private float _proportionToEvict;

        public int population => _model.count;

        public UnityEvent onPopulationExterminated {
            get => _OnPopulationExterminated;
            set => _OnPopulationExterminated = value; 
        }
        public UnityEvent onEmergenceOfPopulation {
            get => _OnEmergenceOfPopulation;
            set => _OnEmergenceOfPopulation = value;
        }
        public IntUnityEvent onPopulationIncreased {
            get => _OnPopulationIncreased;
            set => _OnPopulationIncreased = value;
        }

        ///////////////////////////////////////////////////////////

        [Inject]
        public void Construct(IPopulationModel model, IPopulationView view, ITimerHandler timer, 
            int populationPerInterval, float proportionToEvict)
        {
            _view = view;
            _model = model;
            _timer = timer;
            _proportionToEvict = proportionToEvict;
            _populationPerInterval = populationPerInterval;

            _model.onChanged += OnPopulationChanged;
            _model.onBecomeZero += OnPopulationExterminated;
            _model.onBecomePositive += OnEmergenceOfPopulation;

            _timer.onCircleFinished += IncreasePopulation;
        }

        ///////////////////////////////////////////////////////////

        #region EVENTS_HANDLING
        private void IncreasePopulation()
        {
            _model.Increase(_populationPerInterval);
            onPopulationIncreased?.Invoke(_populationPerInterval);

            Logging.Log(_source, "Population increased");
        }

        private void OnPopulationExterminated()
        {
            _view.Hide();
            _timer.Stop();

            onPopulationExterminated?.Invoke();
            Logging.Log(_source, "Population has been exterminated");
        }

        private void OnEmergenceOfPopulation()
        {
            _view.Show();
            _timer.Launch();

            onEmergenceOfPopulation?.Invoke();
            Logging.Log(_source, "Population has started the emergence");
        }

        private void OnPopulationChanged(int population)
        {
            _view.UpdateView(population);
        }
        #endregion // EVENTS_HANDLING

        #region MONO_BEHAVIOUR

        private void Update()
            =>_timer.Update(Time.deltaTime);

        #endregion // MONO_BEHAVIOUR

        ///////////////////////////////////////////////////////////

        /// <returns>Остатки непринятых оппонентов</returns>
        public int AcceptOpponentsAndReturnRemainder(int population)
        {
            var acceptedPopulation = Mathf.Min(_model.count, population);
            _model.Decrease(acceptedPopulation);

            Logging.Log(_source, $"Accept {acceptedPopulation} opponents");
            return population - acceptedPopulation;
        }

        /// <returns>Остатки непринятых союзников</returns>
        public int AcceptAlliesAndReturnRemainder(int population)
        {
            _model.Increase(population);
            Logging.Log(_source, $"Accept {population} allies");

            return population - population;
        }

        public int EvictPopulationForShips()
        {
            var population = Mathf.FloorToInt(_model.count * _proportionToEvict);
            _model.Decrease(population);

            Logging.Log(_source, $"Evict {population} allies");
            return population;
        }

        public void SetPopulation(int population)
        {
            _model.Set(population);
            Logging.Log(_source, $"Set population {population}");
        }
    }
}
