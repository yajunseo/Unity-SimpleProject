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

        if (Input.GetButtonDown("Jump") /*&& isBottom*/)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            isBottom = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isBottom = false;
        }
    }
}
