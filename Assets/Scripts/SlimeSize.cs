using System;
using UnityEngine;

public class SlimeSize : MonoBehaviour
{
    private float slimeSize;

    private void Start()
    {
         ResetSlimeSize();
    }

    public void ResetSlimeSize()
    {
        slimeSize = 1;
        UpdateSize();
    }

    private void UpdateSize()
    {
        transform.localScale = new Vector3(transform.localScale.x * slimeSize, transform.localScale.y * slimeSize, 1);
    }

    public void AddSize(float delta)
    {
        slimeSize += delta;
        UpdateSize();
    }
}
