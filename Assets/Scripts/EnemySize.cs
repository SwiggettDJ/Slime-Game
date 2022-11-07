using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySize : EntityBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        startSize = .5f;
        ResetSize();
    }
}
