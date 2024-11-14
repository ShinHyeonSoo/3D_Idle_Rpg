using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    public ParticleSystem _effectParticle;

    public RectTransform _effectUI;

    protected override void Awake()
    {
        base.Awake();
    }

    public void CreateFx(Vector3 position)
    {
        ParticleSystem particleSystem = _effectParticle;

        particleSystem.transform.position = position;
        ParticleSystem.EmissionModule em = particleSystem.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, 10));
        ParticleSystem.MainModule mm = particleSystem.main;
        mm.startSpeedMultiplier = 50f;
        particleSystem.Play();
    }
}
