﻿using Core.Interfaces;
using Core.Structs.Figures;
using Galcon.Level.PlayerManagement.Ownership;
using Galcon.Level.Shipping;
using UnityEngine;

namespace Galcon.Level.Planets
{
    interface IPlanet : IGameObjectHost
    {
        IPlanetOwner owner { get; }
        Circle circle { get; }

        void SetRadius(float radius);
        void SetColor(Color color);
        void SetSprite(Sprite sprite);
        void SetSelected(bool status);
        void SetPopulation(int population);

        void SendShips(IPlanet targetPlanet);
        void AcceptShip(IShip ship);

        bool Contains(Vector2 point);
        float DistanceFromCenter(Vector2 point);
        float DistanceFromBorder(Vector2 point);
    }
}