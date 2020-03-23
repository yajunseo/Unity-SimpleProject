using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    [SerializeField] float angle;

    [SerializeField] float speed = 10f;

    Vector2 moveVec;

    Vector2 rightBorder, leftBorder;

    [SerializeField] bool isPlayer; //플레이어가 쏜 탄인지 아닌지에 대한 정보

    [SerializeField] float damage; //입힐 데미지

    bool isAttcked; //피격처리가 된 탄알인지에 대한 정보 

    [SerializeField] GameObject effectPrefab;


    // Start is called before the first frame update
    void Start()
    {
        leftBorder = GameObject.Find("LeftBorder").transform.position;
        rightBorder = GameObject.Find("RightBorder").transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveVec * speed * Time.deltaTime, Space.World);

        DestroyOutBorder();
    }

    void DestroyOutBorder()
    {
        if(transform.position.x > rightBorder.x + 1f
            || transform.position.y > rightBorder.y +1f
            || transform.position.x < leftBorder.x -1f
            || transform.position.y < leftBorder.y - 1f)
        {
            Destroy(gameObject);
        }
    }

    public void SetBullet(float angle)
    {
        this.angle = angle;

        moveVec.x = Mathf.Cos(angle * Mathf.Deg2Rad);
        moveVec.y = Mathf.Sin(angle * Mathf.Deg2Rad);

        transform.right = moveVec;

        transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isAttcked == false)//아직 공격처리가 안된 탄알일때
        {
            
            if(isPlayer == true) //플레이어가 쏜 탄알일때
            {
                EnemyCtrl enemy = collision.GetComponent<EnemyCtrl>();

                if(enemy != null)
                {
                    isAttcked = true;

                    //이펙트 생성
                    GameObject effect = Instantiate(effectPrefab);
                    effect.transform.position = transform.position;


                    //적에게 데미지 입히기
                    enemy.OnDamaged(damage);

                    Destroy(gameObject);

                }

                //if(collision.tag == "Enemy")
                //{

                //}


            }
            else // isPlayer == false -> 적이 쏜 탄알일때
            {
                if(collision.CompareTag("Player") )
                {
                    bool isSuccess;// 공격 성공 여부

                    //1번 방식
                    PlayerCtrl player = collision.GetComponent<PlayerCtrl>();
                    isSuccess = player.OnDamaged(damage);

                    //2번 방식
                    //collision.SendMessage("OnDamaged", damage);

                    if(isSuccess)
                    {
                        //이펙트 생성
                        GameObject effect = Instantiate(effectPrefab);
                        effect.transform.position = transform.position;


                        Destroy(gameObject);

                        isAttcked = true;
                    }

                    

                }

            }



        }

    }


}
