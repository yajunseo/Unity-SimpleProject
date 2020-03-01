using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTranform;
    Vector3 Offset;

    // Start is called before the first frame update
    void Awake()
    {
        playerTranform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTranform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = playerTranform.position + Offset;
    }
}
