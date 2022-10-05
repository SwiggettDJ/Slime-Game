using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class SlimeMovement : MonoBehaviour
{
    private CharacterController playerController;
    public Joystick joystick;
    public float speed = 6f;
    private bool isMoving = false;

    public UnityEvent MovementStartEvent, MovementEndEvent;

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
            if (!isMoving)
            {
                MovementStartEvent.Invoke();
                isMoving = true;
            }
        }
        else
        {
            if (isMoving)
            {
                MovementEndEvent.Invoke();
                isMoving = false;
            }
        }
        playerController.Move(Physics.gravity* Time.deltaTime);
    }
}
