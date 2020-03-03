using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 0.5f;
    public float jumpPower = 5f;
    Rigidbody2D rigid;
    Animator animator;
    bool isBottom;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isBottom = true;
        animator.SetBool("isBottom", isBottom);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        transform.Translate(x * speed * Time.deltaTime, 0, 0);

        if (x > 0)
            transform.localScale = new Vector3(1, 1, 1);

        else if(x<0)
            transform.localScale = new Vector3(-1, 1, 1);



        if (x != 0)
            animator.SetBool("isRun", true);

        else
            animator.SetBool("isRun", false);

        if (Input.GetButtonDown("Jump") && isBottom)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }

        //Landing Platform
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1f, LayerMask.GetMask("Floor"));

        if(rayHit.collider != null)
        {
            if(rayHit.distance<0.5f)
                Debug.Log(rayHit.collider.name);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isBottom = true;
            animator.SetBool("isBottom", isBottom);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isBottom = true;
            animator.SetBool("isBottom", isBottom);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isBottom = false;
            animator.SetBool("isBottom", isBottom);
        }
    }
}
