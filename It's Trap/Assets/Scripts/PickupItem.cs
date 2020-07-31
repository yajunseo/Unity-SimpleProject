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

            int extra = 0;

            if(item.itemType == ItemType.Score)
            {
                SoundManager.instance.PlaySE("Score");
                extra = item.extraScore;
                ScoreManager.extraScore += extra;
            }

            else if (item.itemType == ItemType.NormalGun_Bullet)
            {
                SoundManager.instance.PlaySE("Bullet");
                extra = item.extraBullet;
                guns[NORMAL_GUN].bulletCount += extra;
                theGC.BulletUiSetting();
            }

            string message = "+" + extra;

            FloatingTextManager.instance.CreateFloatingText(other.transform.position, message);

            Destroy(other.gameObject);
        }
    }
}
