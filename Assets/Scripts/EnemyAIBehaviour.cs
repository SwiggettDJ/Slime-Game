using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAIBehaviour : MonoBehaviour
{
    private CharacterController enemyController;

    private float gravity = -10f;
    private float speed = 1.5f;
    public float movementRange = 5f;
    private bool isWaiting;
    private float startPos;
    
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<CharacterController>();
        startPos = transform.position.x;
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

        if (!isWaiting)
        {
            direction.x = speed * Random.Range(-1,2);
            StartCoroutine(Wait(Random.Range(.5f,1.5f)));
        }
        
        if (transform.position.x > startPos + movementRange)
        {
            direction.x = speed * -1;
        }

        if (transform.position.x < startPos - movementRange)
        {
            direction.x = speed;
        }
        
        enemyController.Move(direction * Time.deltaTime);
        if (Mathf.Abs(direction.x) > 0)
        {
            direction.x += Math.Sign(direction.x) * -1 * Time.deltaTime;
        }
    }
    
    private IEnumerator Wait(float time)
    {
        isWaiting = true;
        yield return new WaitForSeconds(time);
        isWaiting = false;
    }
}
