using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("현재 장착된 총")]
    [SerializeField] Gun currentGun;

    float currentFireRate;  // 0이 될때 발사 가능

    // Start is called before the first frame update
    void Start()
    {
        currentFireRate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FireRateCalc();
        TryFire();
        LockOnMouse();
    }

    void FireRateCalc()
    {
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime; // 약 1/60
        }
    }

    void TryFire()
    {
        if(Input.GetButton("Fire1"))
        {
            if (currentFireRate <= 0)
            {
                currentFireRate = currentGun.fireRate;
                Fire();
            }
        }
    }

    void Fire()
    {
        Debug.Log("총알 발사");
        currentGun.animator.SetTrigger("GunFire");

        SoundManager.instance.PlaySE(currentGun.sound_Fire);

        currentGun.ps_MuzzleFlash.Play();
        var clone = Instantiate(currentGun.go_Bullet_Prefab, currentGun.ps_MuzzleFlash.transform.position, Quaternion.identity);  // 어떤 타입인지 모를때 var 타입을 사용
        clone.GetComponent<Rigidbody>().AddForce(transform.forward * currentGun.speed);
    }
    void LockOnMouse()
    {
        Vector3 cameraPos = Camera.main.transform.position;     // Camera 카메라와 관련된 클래스,   main  카메라중에 main 테그로 되어있는 것
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraPos.x));
        // 카메라 화면상의 좌표를 실제 3d 월드상의 좌표로

        Vector3 target = new Vector3(0f, mousePos.y, mousePos.z);
        transform.LookAt(target);
    }
}
