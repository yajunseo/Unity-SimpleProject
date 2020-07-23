using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("피격 이펙트")]
    [SerializeField] GameObject go_RicichetEfeect;

    [Header("총알 데미지")]
    [SerializeField] int damage;

    void OnCollisionEnter(Collision other)
    {
        ContactPoint contactPoint = other.contacts[0];  // 부딪친 객체의 가장 가까운 접촉 면

        var clone = Instantiate(go_RicichetEfeect, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));   // LookRotation 특정 방향을 바라보게 만드는 메소드   contactPoint.normal 충돌한 컬라이더의 표면 방향

        Destroy(clone, 0.5f);
        Destroy(gameObject);
    }
}
