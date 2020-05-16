﻿using UnityEngine;

namespace Galcon.Level.Planets.Creation.Parameters
{
    [CreateAssetMenu(fileName = "PlanetsGeneratorParameters", menuName = "Parameters/PlanetsGenerator")]
    class PlanetsGeneratorParameters : ScriptableObject, IPlanetsGeneratorParameters
    {
        [SerializeField] private GameObject _PlanetPrefab;
        [SerializeField][Min(0)] private float _MinPlanetRadius;
        [SerializeField][Min(0)] private float _MaxPlanetRadius;
        [SerializeField][Min(0)] private float _MinDistanceBetweenPlanetsBorders;
        [SerializeField][Min(0)] private int _MinPlanetsCount;
        [SerializeField][Min(0)] private int _MaxPlanetsCount;
        [SerializeField] private Sprite[] _PossibleSprites;

        ///////////////////////////////////////////////

        public GameObject planetPrefab => _PlanetPrefab;
        public float minPlanetRadius => _MinPlanetRadius;
        public float maxPlanetRadius => _MaxPlanetRadius;
        public float minDistanceBetweenPlanetsBorders => _MinDistanceBetweenPlanetsBorders;
        public int minPlanetsCount => _MinPlanetsCount;
        public int maxPlanetsCount => _MaxPlanetsCount;
        public Sprite[] possibleSprites => _PossibleSprites;
    }
}