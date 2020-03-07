using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;
    BoxCollider2D box;
    public int nextMove;

    // Start is called before the first frame update

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        Think();

        Invoke("Think", 5);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1f, LayerMask.GetMask("Floor"));

        if (rayHit.collider == null)
        {
            Turn();
           
        }

        //if(nextMove > 0)
        //{
        //    transform.localScale = new Vector3(-1,1,1);
        //}

        //else if(nextMove<0)
        //{
        //    transform.localScale = new Vector3(1, 1, 1);
        //}
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);
        float nextThinkTime = Random.Range(2f, 5f);
        

        animator.SetInteger("WalkSpeed", nextMove);
        if(nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        Invoke("Think", nextThinkTime);

    }
    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 2);
    }

    public void OnDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.flipY = true;
        box.enabled = false;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        Invoke("DeActive", 5);

    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}


