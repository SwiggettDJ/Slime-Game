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
    public float gravity = -10f;
    private float maxGravity = -15f;
    public float jumpStrength = 5f;
    private float jumpThreshold = 0.2f;
    private Vector3 direction;

    public UnityEvent MovementStartEvent, MovementEndEvent, FacingRightEvent, FacingLeftEvent;

    private void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        float size = playerController.height;
        
        if (playerController.isGrounded)
        {
            direction.y = 0;
        }

        if (horizontal < 0) FacingLeftEvent.Invoke();
        else if(horizontal > 0) FacingRightEvent.Invoke();
        
        if (Mathf.Abs(horizontal) >= 0.1f)
        {
            direction.x = horizontal * speed;
            if (!isMoving)
            {
                MovementStartEvent.Invoke();
                isMoving = true;
            }
        }
        else
        {
            direction.x = 0;
            if (isMoving)
            {
                MovementEndEvent.Invoke();
                isMoving = false;
            }
        }
        if (vertical >= jumpThreshold && playerController.isGrounded)
        {
            //jump strength should be proportional to size of slime
            direction.y = jumpStrength * size;
        }

        if (!playerController.isGrounded)
        {
            direction.y += gravity * Mathf.Clamp(size, 0.2f, 1.5f) * Time.deltaTime;
            direction.y = Mathf.Clamp(direction.y, maxGravity, Single.PositiveInfinity);
        }
        playerController.Move(direction * Time.deltaTime);
        //playerController.Move(Physics.gravity * gravityMult * Time.deltaTime);
    }
}
