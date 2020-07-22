using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("속도 관련 변수")]
    [SerializeField] float moveSpeed;   // SerializeField    private 여도 강제로 인스펙터 창에 띄움
    [SerializeField] float jetPackSpeed;

    Rigidbody myRigid;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
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
        if(Input.GetKey(KeyCode.Space))
        {
            myRigid.AddForce(Vector3.up * jetPackSpeed);
        }

        else
        {
            myRigid.AddForce(Vector3.down * jetPackSpeed);
        }
    }
}
