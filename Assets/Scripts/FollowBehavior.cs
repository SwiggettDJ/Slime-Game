using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehavior : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        if (!target)
        {
            print("Need to assign a target to Follow Behavior");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position;
    }
}
