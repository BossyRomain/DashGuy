using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovements : MonoBehaviour
{
    private Vector3 velocity;

    [SerializeField]
    private float speed = 200.0f;

    private Vector2 direction = Vector2.zero;

    private Vector2 spawnPoint;

    private bool isMoving = false;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Animator>().SetBool("isMoving", false);
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        SetDirection();

        if(CheckWallInFront())
        {
            StopMoving();
        }

        Vector3 targetVelocity = direction * speed * Time.fixedDeltaTime;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.01f);
    }

    private bool CheckWallInFront()
    {
        if(!isMoving)
        {
            return false;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3) (direction * 0.51f), direction, 0.01f, LayerMask.GetMask("Walls"));

        return hit.collider != null;
    }

    private void SetDirection()
    {
        if(!isMoving)
        {
            bool flipX = false;
            float rotZ = 0f;

            if (Input.GetKey(KeyCode.DownArrow))
            {
                direction = Vector2.down;
                rotZ = 270f;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                direction = Vector2.right;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction = Vector2.left;
                flipX = true;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                direction = Vector2.up;
                rotZ = 90f;
            }

            if (direction != Vector2.zero) { 
                isMoving = true;
                transform.eulerAngles = new Vector3(0, 0, rotZ);
                GetComponent<SpriteRenderer>().flipX = flipX;

                GetComponent<Animator>().SetBool("isMoving", true);

                Debug.Log("Bouge!");
            }
        } 
    }

    private void StopMoving()
    {
        Debug.Log("Stop!");
        isMoving = false;
        direction = Vector3.zero;
        GetComponent<Animator>().SetBool("isMoving", false);
    }

    public void Death()
    {
        StopMoving();
        transform.position = spawnPoint;
    }
}
