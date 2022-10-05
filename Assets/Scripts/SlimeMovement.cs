using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class SlimeMovement : MonoBehaviour
{
    private CharacterController playerController;
    public Joystick joystick;
    public float speed = 6f;

    public UnityEvent MovementEvent;

    private void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        if (direction.magnitude >= 0.1f)
        {
            playerController.Move(direction * speed * Time.deltaTime);
            MovementEvent.Invoke();
        }
    }
}
