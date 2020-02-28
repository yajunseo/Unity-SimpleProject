using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float jumpPower = 5f;

    Rigidbody2D rigidbody;
    Animator animator;

    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (isGrounded)
            {
                rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                animator.SetTrigger("Jump");
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            if(collision.contacts[0].normal.y >=0.7f)
            {
                isGrounded = true;
                animator.SetBool("isGrounded", isGrounded);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = false;
            animator.SetBool("isGrounded", isGrounded);
        }
    }
}
