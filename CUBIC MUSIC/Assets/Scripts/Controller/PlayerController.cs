using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 이동
    [SerializeField] float moveSpeed = 3;
    Vector3 dir = new Vector3();
    Vector3 desPos = new Vector3();

    // 회전
    [SerializeField] float spinSpeed = 270;
    Vector3 rotDir = new Vector3();
    Quaternion destRot = new Quaternion();

    // 반동
    [SerializeField] float recoilPosY = 0.25f;
    [SerializeField] float recoilSpeed = 1.5f;


    bool canMove = true;

    // 기타
    [SerializeField] Transform fakeCube = null;
    [SerializeField] Transform realCube = null;

    TimingManager theTimingManager;

    private void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            if (canMove)
            { 
                // 판정 체크
                if (theTimingManager.CheckTiming())
                {
                    StartAction();
                }
            }
        }
    }

    void StartAction()
    {
        // 방향 계산
        dir.Set(Input.GetAxisRaw("Vertical"),0,Input.GetAxisRaw("Horizontal"));

        // 이동 목표값 계산
        desPos = transform.position + new Vector3(-dir.x, 0, dir.z);

        // 회전 목표값 계산
        rotDir = new Vector3(-dir.z, 0f, -dir.x);
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);   // 공전에 쓰임
        destRot = fakeCube.rotation;

        StartCoroutine(MoveCo());
        StartCoroutine(Spin());
        StartCoroutine(RecoilCo());
    }

    IEnumerator MoveCo()
    {
        canMove = false;

        while(Vector3.SqrMagnitude(transform.position - desPos) >= 0.001f) // distance 보다 가벼움 , 제곱근 구하는것
        {
            transform.position = Vector3.MoveTowards(transform.position, desPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = desPos;

        canMove = true;
    }

    IEnumerator Spin()
    {
        while(Quaternion.Angle(realCube.rotation, destRot)>0.5f)
        {
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed * Time.deltaTime);
            yield return null;
        }

        realCube.rotation = destRot;
    }

    IEnumerator RecoilCo()
    {
        while (realCube.position.y < recoilPosY)
        {
            realCube.position += new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        while (realCube.position.y > 0)
        {
            realCube.position -= new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        realCube.localPosition = new Vector3(0, 0, 0);
    }
}
