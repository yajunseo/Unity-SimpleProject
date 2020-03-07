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
    SpriteRenderer sprite;
    public GameManager Manager;
    BoxCollider2D box;

    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        audio = GetComponent<AudioSource>();
        animator.SetBool("isBottom", isBottom);
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audio.clip = audioJump;
                break;
            case "ATTACK":
                audio.clip = audioAttack;
                break;
            case "DAMAGED":
                audio.clip = audioDamaged;
                break;
            case "ITEM":
                audio.clip = audioItem;
                break;
            case "DIE":
                audio.clip = audioDie;
                break;
            case "FINISH":
                audio.clip = audioFinish;
                break;
        }
        audio.Play();
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
            PlaySound("JUMP");
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

        if(collision.gameObject.tag == "Enemy")
        {
            if(rigid.velocity.y<0 && transform.position.y >collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else
                OnDamaged(collision.transform.position);
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

    void OnDamaged(Vector2 targetPos)
    {
        Manager.HealthDown();
        gameObject.layer = 11;
        PlaySound("DAMAGED");
        sprite.color = new Color(1, 1, 1, 0.4f);
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc,1) * 7, ForceMode2D.Impulse);

        animator.SetTrigger("Damaged");

        Invoke("OffDamaged", 2);

    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        sprite.color = new Color(1, 1, 1, 1);
    }

    void OnAttack(Transform enemy)
    {
        PlaySound("ATTACK");
        Manager.stagePoint += 100;
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }

    public void OnDie()
    {
        PlaySound("DIE");
        sprite.color = new Color(1, 1, 1, 0.4f);
        sprite.flipY = true;
        box.enabled = false;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            PlaySound("ITEM");
            bool isBronze = collision.gameObject.name.Contains("B");
            bool isSilver = collision.gameObject.name.Contains("S");
            bool isGold = collision.gameObject.name.Contains("G");

            if(isBronze)
                Manager.stagePoint += 50;
            else if (isSilver)
                Manager.stagePoint += 100;
            else if (isGold)
                Manager.stagePoint += 300;


            collision.gameObject.SetActive(false);
        }

        else if (collision.gameObject.tag == "Finish")
        {
            PlaySound("FINISH");
            collision.gameObject.SetActive(false);
            Time.timeScale = 0;
        }

    }
}
