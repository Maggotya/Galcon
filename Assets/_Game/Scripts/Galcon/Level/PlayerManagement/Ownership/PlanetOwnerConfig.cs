using System;
using UnityEngine;

namespace Galcon.Level.PlayerManagement.Ownership
{
    [Serializable]
    struct PlanetOwnerConfig : IPlanetOwnerConfig
    {
        [SerializeField] private string _OwnerTag;
        [SerializeField] private Color _OwnerColor;

        public string ownerTag => _OwnerTag;
        public Color ownerColor => _OwnerColor;

        ///////////////////////////////////////////

        public PlanetOwnerConfig(string tag, Color color)
        {
            _OwnerTag = tag;
            _OwnerColor = color;
        }
    }
}
