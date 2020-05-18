using System.Linq;
using Core;
using Core.Extensions;
using Core.Structs.Ranges;
using UnityEngine;

namespace Galcon.Level.ScreenView
{
    public class BorderedArea : MyMonoBehaviour, IBorderedArea
    {
        public IBorder left { get; private set; }
        public IBorder right { get; private set; }
        public IBorder top { get; private set; }
        public IBorder bottom { get; private set; }

        public IBorder[] borders { get; private set; }
        public SquareRange area { get; private set; }

        //////////////////////////////////////////////////

        private void OnEnable()
            => Initialize();

        //////////////////////////////////////////////////

        private void Initialize()
        {
            borders = GetComponentsInChildren<IBorder>(true);

            left    = GetBorder(BorderType.Left);
            right   = GetBorder(BorderType.Right);
            top     = GetBorder(BorderType.Top);
            bottom  = GetBorder(BorderType.Bottom);

            area = new SquareRange(
                new Range(left.position.x, right.position.x),
                new Range(bottom.position.y, top.position.y) );

            Logging.Log(_source, "Initialized");
        }

        //////////////////////////////////////////////////

        public IBorder GetBorder(BorderType type)
            => borders.FirstOrDefault(b => b.type == type) ?? 
            new FakeBorder(type, Vector2.one * Mathf.Infinity, Vector2.zero);

        public float[] GetDistanciesToBorders(Vector2 point)
            => borders.Select(b => b.GetDistance(point)).ToArray();

        public float GetMinDistanceToBorder(Vector2 point)
            => GetDistanciesToBorders(point).Min();
    }
}
