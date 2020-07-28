using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("속도 관련 변수")]
    [SerializeField] float moveSpeed;   // SerializeField    private 여도 강제로 인스펙터 창에 띄움
    [SerializeField] float jetPackSpeed;

    Rigidbody myRigid;

    public bool IsJet { get; private set; } // 은닉성 보장 property;

    [Header("파티클 시스템(부스터)")]
    [SerializeField] ParticleSystem ps_LeftEngine;
    [SerializeField] ParticleSystem ps_RightEngine;

    AudioSource AudioSource;

    JetEngineFuelManager theFuel;

    // Start is called before the first frame update
    void Start()
    {
        IsJet = false;
        myRigid = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
        theFuel = FindObjectOfType<JetEngineFuelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        TryMove();
        TryJet();
    }

    void TryMove()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)    // D키 =1 A키 = -1
        {
            Vector3 moveDir = new Vector3(0, 0, Input.GetAxisRaw("Horizontal"));
            myRigid.AddForce(moveDir * moveSpeed);        // 특정 방향으로 힘을 가함
        }
    }

    void TryJet()
    {
        if(Input.GetKey(KeyCode.Space) && theFuel.IsFuel)
        {
            if (!IsJet)
            {
                ps_LeftEngine.Play();
                ps_RightEngine.Play();
                AudioSource.Play();
                IsJet = true;
            }

            myRigid.AddForce(Vector3.up * jetPackSpeed);
        }

        else
        {
            if (IsJet)
            {
                ps_LeftEngine.Stop();
                ps_RightEngine.Stop();
                AudioSource.Stop();
                IsJet = false;
            }


            myRigid.AddForce(Vector3.down * jetPackSpeed);
        }
    }
}
