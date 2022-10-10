using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SlimeSize : MonoBehaviour
{
    private float slimeSize;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        ResetSlimeSize();
    }

    public void ResetSlimeSize()
    {
        slimeSize = 1;
        UpdateSize();
    }

    private void UpdateSize()
    {
        transform.localScale = new Vector3(slimeSize, slimeSize, transform.localScale.z);
        controller.height = slimeSize;
        controller.radius = slimeSize / 2;
    }

    public void AddSize(float delta)
    {
        slimeSize += delta;
        UpdateSize();
    }
}
