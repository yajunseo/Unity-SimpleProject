using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [Header("따라갈 대상 지정")]
    [SerializeField]protected Transform tf_Player;

    [Header("따라갈 속도 지정")] [Range(0,1)]
    [SerializeField] protected float speed;

    protected Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = tf_Player.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(this.transform.position, tf_Player.transform.position - currentPos, speed); // 위치 a와 b사이의 값을 speed 비율로 보간
    }
}
