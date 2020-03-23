using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    [SerializeField] float hp = 100f;

    SpriteRenderer renderer;

    bool isDie; //생사여부

    public bool isCanFire; //공격이 가능한 여부

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject explosionPrefab;

    float currentFireTime;

    [SerializeField] float refireTime = 5f;


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(isCanFire == true)
        {
            //currentFireTime += Time.deltaTime;

            //if (currentFireTime > refireTime)
            //{
            //    //발사
            //    FireType1();

            //    currentFireTime = 0;
            //}

            StartCoroutine(FireType3());

        }

    }

    IEnumerator FireType3()
    {
        isCanFire = false;

        FireBullet(-90);

        yield return new WaitForSeconds(refireTime); //refireTime만큼 현재 함수의 코드가 멈춘다.

        isCanFire = true;

    }



    public void OnDamaged(float damage) //Bullet스크립트에서 호출할 함수
    {
        if(isDie == false)
        {
            hp -= damage;
            renderer.color = Color.red;

            Invoke("ReturnColor", 0.1f);

            if(hp <= 0)
            {
                isDie = true;

                //폭발 이펙트
                GameObject effect = Instantiate(explosionPrefab);
                effect.transform.position = transform.position;


                //스코어 처리

                //카메라 쉐이크

                Destroy(gameObject);
            }

        }

    }

    void ReturnColor()
    {
        renderer.color = Color.white;

    }

    void FireType1()
    {
        FireBullet(-90);

    }

    void FireType2()
    {
        for (int i = 0; i < 360; i += 10)
        {
            FireBullet(i);
        }

    }

    void FireBullet(float angle)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;

        bullet.GetComponent<BulletCtrl>().SetBullet(angle);
    }

}
