using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool s_canPresskey = true;

    // 이동
    [SerializeField] float moveSpeed = 3;
    Vector3 dir = new Vector3();
    public Vector3 desPos = new Vector3();
    Vector3 orginPos = new Vector3();

    // 회전
    [SerializeField] float spinSpeed = 270;
    Vector3 rotDir = new Vector3();
    Quaternion destRot = new Quaternion();

    // 반동
    [SerializeField] float recoilPosY = 0.25f;
    [SerializeField] float recoilSpeed = 1.5f;


    bool canMove = true;
    bool isFalling = false;

    // 기타
    [SerializeField] Transform fakeCube = null;
    [SerializeField] Transform realCube = null;

    TimingManager theTimingManager;
    CameraController theCam;
    Rigidbody myRigid;
    StatusManager theStatus;

    private void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        theCam = FindObjectOfType<CameraController>();
        myRigid = GetComponentInChildren<Rigidbody>();
        orginPos = transform.position;
        theStatus = FindObjectOfType<StatusManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckFalling();

        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            if (canMove & s_canPresskey && !isFalling)
            {
                Calc();

                // 판정 체크
                if (theTimingManager.CheckTiming())
                {
                    StartAction();
                }
            }
        }
    }

    void Calc()
    {
        // 방향 계산
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        // 이동 목표값 계산
        desPos = transform.position + new Vector3(-dir.x, 0, dir.z);

        // 회전 목표값 계산
        rotDir = new Vector3(-dir.z, 0f, -dir.x);
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);   // 공전에 쓰임
        destRot = fakeCube.rotation;
    }

    void StartAction()
    {
 

        StartCoroutine(MoveCo());
        StartCoroutine(Spin());
        StartCoroutine(RecoilCo());
        StartCoroutine(theCam.ZoomCam());
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

    void CheckFalling()
    {
        if(!isFalling && canMove)
        {
            if (!Physics.Raycast(transform.position, Vector3.down, 1.1f))
            {
                Falling();
            }
        }
    }

    void Falling()
    {
        isFalling = true;
        myRigid.useGravity = true;
        myRigid.isKinematic = false;
    }

    public void ResetFalling()
    {
        theStatus.DecreaseHp(1);

        AudioManger.instance.PlaySFX("Falling");

        if (!theStatus.IsDead())
        {
            isFalling = false;
            myRigid.useGravity = false;
            myRigid.isKinematic = true;

            transform.position = orginPos;
            realCube.localPosition = new Vector3(0, 0, 0);
        }
    }
}
