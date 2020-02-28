using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMove : MonoBehaviour
{
    public float speed = 0.2f;
    public float jumpPower = 10f;

    Vector2 direction;

    Rigidbody2D rigidbody;
    SpriteRenderer renderer;

    float originScaleX;

    bool isGrounded;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        originScaleX = transform.localScale.x;

        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //ProjectSetting - Input에 있는 키설정을 토대로 값을 가져옴.
        float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");

        //반전시키기
        if(x > 0)//오른쪽으로 이동하고 있을때
        {
            Vector3 setScale = Vector3.zero;
            setScale.Set(originScaleX, transform.localScale.y, transform.localScale.z);

            transform.localScale = setScale; //new를 쓴 이유는 Vector3의 생성자를 이용하기위해 사용됨.(값복사)
            //renderer.flipX = false;

        }
        else if( x < 0)//왼쪽으로 이동하고 있을때
        {
            Vector3 setScale = Vector3.zero;
            setScale.Set(-originScaleX, transform.localScale.y, transform.localScale.z);

            transform.localScale = setScale;
            //renderer.flipX = true;
        }
        

        direction.x = x;

        transform.Translate(direction * speed, Space.World);

        //점프하기
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded == true)
             rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            animator.SetTrigger("Jump");
        }

        if(x != 0)
        {
            animator.SetBool("isRun", true);
        }

        else
        {
            animator.SetBool("isRun", false);
        }

    }

    //충돌함수 호출 조건
    //1. 두 물체중 하나는 Rigidbody가 있어야하고,
    //2. 두 물체 모두 Collider 컴포넌트가 있어야 한다.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Cloud")
        {
            //땅을 밟고 있다고 처리, 즉 점프가 가능해지게 되는 부분
            isGrounded = true;
        }

        if(collision.gameObject.tag == "Flag")
        {
            Destroy(collision.gameObject);
        }

    }

    //충돌을 했다가 벗어날때 호출이 되는 함수
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Cloud")
        {
            isGrounded = false;
        }
    }

    //충돌한 상태로 머무르고 있을때 호출이 되는 함수
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }


    //함수 호출 조건
    //1. 두 물체 중 하나는 Rigidbody2D가 있어야한다.
    //2. 두 물체 모두 Collider가 있어야한다.
    //3. 두 물체 중 하나는 isTrigger인 Collider가 있어야 한다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Flag")
        {
            Destroy(collision.gameObject);
        }

    }


}
