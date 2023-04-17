using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSprite : MonoBehaviour
{
    private Animator[] anims;
    private float delay = 0;
    void Start()
    {
        delay = Random.Range(0, .4f)*2;
        anims = GetComponentsInChildren<Animator>();

        foreach (Animator anim in anims)
        {
            StartCoroutine(PlayDelay(anim));
            delay += .2f;
        }
    }

    private IEnumerator PlayDelay(Animator anim)
    {
        anim.enabled = false;
        yield return new WaitForSeconds(delay);
        anim.enabled = true;
    }
}
