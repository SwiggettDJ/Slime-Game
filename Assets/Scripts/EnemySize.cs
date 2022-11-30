using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySize : EntityBehaviour
{
    private Animator enemyAnimator;
    private ParticleSystem ps;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ResetSize();
        enemyAnimator = GetComponentInChildren<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    public void Damaged()
    {
        StartCoroutine(GetComponentInChildren<ParticleHandler>().EnableForces());
        ps.Emit((int)Mathf.Clamp(size*10, 5, Single.PositiveInfinity));
        AddSize(-size/3);
        enemyAnimator.SetTrigger("Shrink");
    }
}
