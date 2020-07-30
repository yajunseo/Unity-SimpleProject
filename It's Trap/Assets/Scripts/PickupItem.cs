using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] Gun[] guns;

    GunController theGC;

   void Start()
    {
        theGC = FindObjectOfType<GunController>(); 
    }

    const int NORMAL_GUN = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();

            if(item.itemType == ItemType.Score)
            {
                SoundManager.instance.PlaySE("Score");
                ScoreManager.extraScore += item.extraScore;
            }

            else if (item.itemType == ItemType.NormalGun_Bullet)
            {
                SoundManager.instance.PlaySE("Bullet");
                guns[NORMAL_GUN].bulletCount += item.extraBullet;
                theGC.BulletUiSetting();
            }

            Destroy(other.gameObject);
        }
    }
}
