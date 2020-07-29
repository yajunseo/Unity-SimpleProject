using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("피격 이펙트")]
    [SerializeField] GameObject go_RicichetEfeect;

    [Header("총알 데미지")]
    [SerializeField] int damage;

    [Header("피격 효과음")]
    [SerializeField] string sound_Ricochet;

    void OnCollisionEnter(Collision other)
    {
        ContactPoint contactPoint = other.contacts[0];  // 부딪친 객체의 가장 가까운 접촉 면

        SoundManager.instance.PlaySE("NormalGun_Ricochet");

        var clone = Instantiate(go_RicichetEfeect, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));   // LookRotation 특정 방향을 바라보게 만드는 메소드   contactPoint.normal 충돌한 컬라이더의 표면 방향

        if(other.transform.CompareTag("Mine"))
        {
            other.transform.GetComponent<Mine>().Damaged(damage);
        }


        Destroy(clone, 0.5f);
        Destroy(gameObject);
    }
}
