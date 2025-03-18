using System;
using UnityEngine;

[Serializable]
public struct SnowfallSettings
{
    [SerializeField] private float _lifetimeSeconds; public float LifetimeSeconds => _lifetimeSeconds;
    [SerializeField] private ParticleSystem.MinMaxCurve _speed; public ParticleSystem.MinMaxCurve Speed => _speed;
    [SerializeField] private float _emission; public float Emission => _emission;
}
