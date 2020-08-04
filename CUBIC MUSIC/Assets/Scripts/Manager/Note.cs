using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;  // local 을 안하면 캔버스 좌표가 아닌 월드 좌표로 이동하니 주의!!!
    }
}
