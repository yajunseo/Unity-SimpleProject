using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    Vector2 leftBorder, rightBorder;

    Vector2 moveVec;

    [SerializeField] float speed = 5f;

    Vector2 setPos; //경계지점을 지났을때 지정해줄 위치

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject explosionPrefab;

    float hp = 100;

    SpriteRenderer renderer;

    bool isDie;

    bool isCanDamaged;

    // Start is called before the first frame update
    void Start()
    {
        leftBorder = GameObject.Find("LeftBorder").transform.position;
        rightBorder = GameObject.Find("RightBorder").transform.position;

        renderer = GetComponent<SpriteRenderer>();


        StartCoroutine(ReBirth());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        BlockMove2();

        if(Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet(90);
        }

    }
    
    IEnumerator ReBirth()
    {
        int count = 0;
        int maxCount = 10;

        while(count < maxCount)
        {
            count++;
            renderer.color = Color.clear;

            yield return new WaitForSeconds(0.1f);
            renderer.color = Color.white; // 1,1,1   원래색을 의미함

            yield return new WaitForSeconds(0.1f);
        }

        isCanDamaged = true;
    }


    void Move()
    {
        moveVec.x = Input.GetAxisRaw("Horizontal");
        moveVec.y = Input.GetAxisRaw("Vertical");

        transform.Translate(moveVec.normalized * speed * Time.deltaTime, Space.World);
        //moveVec.normalized는 moveVec을 단위벡터(크기가 1인 벡터)화한 값
        
    }

    void BlockMove()
    {
        if(transform.position.x > rightBorder.x)//우측으로 벗어날때
        {
            setPos.x = rightBorder.x;
            setPos.y = transform.position.y;

            transform.position = setPos;
        }

        if(transform.position.y > rightBorder.y)//상단을 벗어날때
        {
            setPos.x = transform.position.x;
            setPos.y = rightBorder.y;

            transform.position = setPos;
        }
        
        if(transform.position.x < leftBorder.x)//좌측을 벗어날때
        {
            setPos.x = leftBorder.x;
            setPos.y = transform.position.y;

            transform.position = setPos;
        }

        if(transform.position.y < leftBorder.y)//하단을 벗어날때
        {
            setPos.x = transform.position.x;
            setPos.y = leftBorder.y;

            transform.position = setPos;
        }

    }

    void BlockMove2()
    {
        //Clamp는 최솟값과 최댓값으로 잘라서 반환해주는 함수
        setPos.x = Mathf.Clamp(transform.position.x , leftBorder.x, rightBorder.x);
        setPos.y = Mathf.Clamp(transform.position.y , leftBorder.y, rightBorder.y);

        transform.position = setPos;
    }

    void FireBullet(float angle)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position; //탄알의 위치를 플레이어의 위치로 조정

        bullet.GetComponent<BulletCtrl>().SetBullet(angle);
    }

    public bool OnDamaged(float damage)
    {
        if (isDie == false && isCanDamaged)
        {
            hp -= damage;
            renderer.color = Color.red;

            Invoke("ReturnColor", 0.1f);

            {
            if (hp <= 0)
                isDie = true;

                //폭발 이펙트
                GameObject effect = Instantiate(explosionPrefab);
                effect.transform.position = transform.position;

                //게임 오버
                GameManager.instance.PlayerDie();


                //카메라 쉐이크

                Destroy(gameObject);
            }
            return true;
        }

        else
            return false;
    }

    void ReturnColor()
    {
        renderer.color = Color.white;
    }

}
