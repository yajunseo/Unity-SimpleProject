using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int currentScore; // 현제 점수
    public int GetCurrentScore() { return currentScore; }

    public void ResetCurrentScore() { currentScore = 0;distanceScore = 0; maxDistance = 0;extraScore = 0; }

    public static int extraScore; // 아이템 점수
    int distanceScore; // 거리 점수
    float maxDistance; //플레이어가 이동한 최대 거리
    float originPosZ; // 플레이어의 최초 위치의 z값

    [SerializeField] Text txt_Score;
    [SerializeField] Transform tf_Player; // 플레이어의 위치 정보

    void Start()
    {
        originPosZ = tf_Player.position.z;    
    }

    // Update is called once per frame
    void Update()
    {
        if(tf_Player.position.z >maxDistance)
        {
            maxDistance = tf_Player.position.z;
            distanceScore = Mathf.RoundToInt(maxDistance - originPosZ);
        }

        currentScore = extraScore + distanceScore;
        txt_Score.text = string.Format("{0:000,000}", currentScore);
    }
}
