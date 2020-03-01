using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBall : MonoBehaviour
{
    Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        //rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetButton("Jump"))
        {
            rigid.AddForce(Vector3.up * 1, ForceMode.Impulse);
        }
        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"),
             0, Input.GetAxisRaw("Vertical"));
        rigid.AddForce(vec, ForceMode.Impulse);

        //rigid.AddTorque(Vector3.up);

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Cube")
        {
            rigid.AddForce(Vector3.up * 2, ForceMode.Impulse);
        }
    }

    public void Jump()
    {
        rigid.AddForce(Vector3.up * 20, ForceMode.Impulse);
    }

    void Update()
    {
        
    }
}
