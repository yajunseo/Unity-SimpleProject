using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    protected Vector2 startPos;

    [SerializeField] protected float speed = 5f;

    protected bool isAwake;

    [SerializeField] protected float awakeDist = 5f;

    protected Vector2 moveVec;



    // Start is called before the first frame update
    protected void Start()
    {
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected void AwakeMove()
    {
        moveVec = Vector2.down;

        if (Vector2.Distance(transform.position, startPos) > awakeDist)
        {
            isAwake = true;

            moveVec = Vector2.right;

            EnemyCtrl enemy = GetComponent<EnemyCtrl>();

            if (enemy != null)
                enemy.isCanFire = true;

            
            //GetComponent<EnemyCtrl>().isCanFire = true;
        }
    }

    protected virtual void Move()
    {
        if(isAwake == false)
        {
            AwakeMove();

        }
        else //어느정도 내려오고 공격패턴이 적용되는 부분
        {
            if( 5 - transform.position.x < 0.1f) //오른쪽으로 어느정도 갔을때
            {
                moveVec = Vector2.left;
            }
            else if( -5 -transform.position.x > 0.1f) //왼쪽으로 어느정도 갔을때
            {
                moveVec = Vector2.right;
            }
        }

        transform.Translate(moveVec * speed * Time.deltaTime, Space.World);

    }

}
