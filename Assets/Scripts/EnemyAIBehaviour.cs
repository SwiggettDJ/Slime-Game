using System;
using System.Collections;
using UnityEngine;

public class EnemyAIBehaviour : MonoBehaviour
{
    private CharacterController enemyController;

    private float gravity = -10f;
    private float speed = 6f;
    public float movementRange = 2f;
    
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyController.isGrounded)
        {
            direction.y += gravity * Time.deltaTime;
            direction.y = Mathf.Clamp(direction.y, -15, Single.PositiveInfinity);
        }
        else
        {
            direction.y = 0;
        }
        
        
        enemyController.Move(direction * Time.deltaTime);
    }   
    
    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
