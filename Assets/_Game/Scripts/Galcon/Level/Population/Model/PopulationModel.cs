using System;
using UnityEngine;

namespace Galcon.Level.Population.Model
{
    struct PopulationModel : IPopulationModel
    {
        private int _count;
        private Action<int> _onChanged;
        private Action _onBecomePositive;
        private Action _onBecomeZero;

        public int count => _count;

        public Action<int> onChanged {
            get => _onChanged;
            set => _onChanged = value;
        }
        public Action onBecomePositive {
            get => _onBecomePositive;
            set => _onBecomePositive = value;
        }
        public Action onBecomeZero {
            get => _onBecomeZero;
            set => _onBecomeZero = value;
        }

        private const int _MIN_VALUE = 0;

        /////////////////////////////////////
    
        public void Increase(int count)
            => Set(_count + count);

        public void Decrease(int count)
            => Set(_count - count);

        public void Set(int count)
        {
            count = Mathf.Max(_MIN_VALUE, count);

            if (_count == count)
                return;

            if (_count == _MIN_VALUE && count > _MIN_VALUE)
                _onBecomePositive?.Invoke();

            if (_count > _MIN_VALUE && count == _MIN_VALUE)
                onBecomeZero?.Invoke();

            _count = count;
            _onChanged?.Invoke(_count);
        }

        public void Reset()
        {
            _count = _MIN_VALUE;
            onBecomeZero?.Invoke();
            _onChanged?.Invoke(_count);
        }
    }
}
