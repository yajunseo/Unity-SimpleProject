using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Animator animator;

    public float speed = 15f;
    public float jumpPower = 7f;

    Vector2 moveVec = Vector2.zero;
    Vector3 scaleVec = Vector3.one;

    bool isRun, isGround;
    int rings;

    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVec.x = Input.GetAxisRaw("Horizontal"); // row가 붙으면 -1,0,1이라는 값으로 떨어진다
        //moveVec.x = Input.GetAxis("Horizontal"); // row가 없는 getAxis는 0부터 1사이, -1부터 0사이의 소수값도 적용된다.

        rigidbody.AddForce(moveVec * speed);

        if (moveVec.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isRun = true;
        }

        else if (moveVec.x > 0)
        {
            transform.localScale = Vector3.one;
            isRun = true;
        }

        else
            isRun = false;

        animator.SetBool("isRun", isRun);

        float speedX = rigidbody.velocity.x;
        speedX = Mathf.Abs(speedX);
        animator.SetFloat("Speed", speedX);

        velocity = rigidbody.velocity;

        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGround = true;

        animator.SetBool("isGrounded", isGround);
;    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGround = false;

        animator.SetBool("isGrounded", isGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        rings++;

        Debug.Log("링 획득!");
        Debug.Log("링의 갯수 : " + rings);
    }
}
