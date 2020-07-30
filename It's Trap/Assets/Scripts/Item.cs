using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Score,
    NormalGun_Bullet,
}


public class Item : MonoBehaviour
{

    public ItemType itemType; // 아이템 유형
    public int extraScore; // 추가 점수
    public int extraBullet; // 추가 획득 총알


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
    }
}
