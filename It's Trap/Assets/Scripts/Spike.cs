using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] int damage;

    [SerializeField] float force;

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.CompareTag("Player"))
        {
            Debug.Log(damage + "를 플레이어에게 입혔습니다");
            other.transform.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, 5f);
            other.transform.GetComponent<StatusManager>().DecreaseHP(damage);
        }
    }
}
