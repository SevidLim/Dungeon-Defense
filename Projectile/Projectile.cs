using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : AbstractProjectile
{
    private TrailRenderer trailRenderer;
    public float timer;

    void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        if(trailRenderer == null)
        {
            return;
        }

        trailRenderer.time = 0.5f;

        trailRenderer.startWidth = 0.1f;

        trailRenderer.endWidth = 0f;
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
