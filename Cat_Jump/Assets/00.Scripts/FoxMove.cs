using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxMove : MonoBehaviour
{
    public float speed = 0.05f;
    public float jumpPower = 14f;

    Rigidbody2D rigidbody;
    float x;
    float originalScaleX;
    Vector2 direction;
    Animator animator;

    bool isLand;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        originalScaleX = transform.localScale.x;

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        Vector3 scale = Vector3.zero;
        if (x>0)
        {
            scale.Set(originalScaleX, transform.localScale.y, transform.localScale.z);
            transform.localScale = scale;
        }

        else if(x<0)
        {
            scale.Set(-originalScaleX, transform.localScale.y, transform.localScale.z);
            transform.localScale = scale;
        }

        if (x != 0)
            animator.SetBool("isRun", true);

        else
            animator.SetBool("isRun", false);

        direction.x = x;

        transform.Translate(direction * speed, Space.World);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isLand)
            {
                rigidbody.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
                isLand = false;
            }
            animator.SetTrigger("Jump");
        }
      

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cloud")
            isLand = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "gem")
            Destroy(collision.gameObject);
    }
}
