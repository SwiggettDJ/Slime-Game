using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSprite : MonoBehaviour
{
    private Animator[] anims;
    void Start()
    {
        GetComponent<Animator>().Update(Random.Range(0,18));
    }
}
