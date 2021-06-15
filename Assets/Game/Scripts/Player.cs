using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private Rigidbody2D plyRB;
    private Animator animator;
    private bool canJump;
    private bool isEnable;
    private float forwardForce = 0f;

    public UnityEvent OnPlayerHitted;
    public float jumpFactor = 5f;
    public float forwardFactor = 0.2f;
    public Transform startPlayerPosition;

    void Start()
    {
        plyRB = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        canJump = true;
        isEnable = false;

    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void SetActive()
    {
        isEnable = true;
        canJump = true;
        animator.Play("Player_running");
        plyRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        gameObject.transform.localPosition = startPlayerPosition.localPosition;
    }

    void Jump()
    {
        if (!isEnable) return;
        if (canJump)
        {
            canJump = false;

            if(transform.position.x < 0)
            {
                forwardForce = forwardFactor; 
            }
            else
            {
                forwardForce = 0f;
            }
            plyRB.velocity = new Vector2(forwardForce, jumpFactor);
            animator.Play("Player_jumping");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isEnable) return;
        if (collision.gameObject.tag == "Obstacle")
        {
            plyRB.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.Play("Player_death");
            isEnable = false;
            canJump = false;
            OnPlayerHitted.Invoke();
        }
        else
        {
            animator.Play("Player_running");
            canJump = true;
        }

    }
}
