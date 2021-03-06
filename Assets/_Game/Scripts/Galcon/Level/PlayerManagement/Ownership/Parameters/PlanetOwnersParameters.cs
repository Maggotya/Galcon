﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Galcon.Level.PlayerManagement.Ownership.Parameters
{
    [CreateAssetMenu(fileName = "PlanetOwnersParameters", menuName = "Parameters/PlanetOwners")]
    public class PlanetOwnersParameters : ScriptableObject, IPlanetOwnersParameters
    {
        [SerializeField] private PlanetOwnerConfig[] _Owners;

        public IPlanetOwnerConfig[] owners => _Owners.Select(c => c as IPlanetOwnerConfig).ToArray();

        private const string _DEFAULT_TAG = "Default";

        //////////////////////////////////////////////////////////

        #region ON_ENABLE_CORRECTIONS
        private void OnEnable()
            => _Owners = AddAbsentDefaultElement(
                GetDistinctedByTagList()).ToArray();

        private List<PlanetOwnerConfig> GetDistinctedByTagList()
            => _Owners
                .Select(c => c.ownerTag)
                .Distinct()
                .Select(t => _Owners.First(c => c.ownerTag == t))
                .ToList();

        private List<PlanetOwnerConfig> AddAbsentDefaultElement(List<PlanetOwnerConfig> configs)
        {
            if (configs.Any(c => c.ownerTag == _DEFAULT_TAG) == false)
                configs.Insert(0, new PlanetOwnerConfig(_DEFAULT_TAG, Color.white));

            return configs;
        }
        #endregion // ON_ENABLE_CORRECTIONS

        //////////////////////////////////////////////////////////

        public IPlanetOwnerConfig GetConfig(string tag)
            => owners.FirstOrDefault(c => c.ownerTag == tag) ?? GetConfig(_DEFAULT_TAG);
    }
}
