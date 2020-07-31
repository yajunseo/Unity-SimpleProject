using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StageManager : MonoBehaviour
{
    [SerializeField] Text txt_CurrentScore;
    [SerializeField] GameObject go_UI;
    [SerializeField] ScoreManager theSM;
    [SerializeField] GameObject[] go_Stages;
    int currentStage = 0;

    [SerializeField] Rigidbody playerRigid;
    [SerializeField] Transform tf_OriginPos;

    public void ShowClearUI()
    {
        PlayerController.camMove = false;
        playerRigid.isKinematic = true;  // 물리 효과를 영향줄지 말지? true 시 꺼짐
        go_UI.SetActive(true);
        txt_CurrentScore.text = string.Format("{0:000,000}", theSM.GetCurrentScore());
    }

    public void NextBtn()
    {
        if (currentStage < go_Stages.Length - 1)
        {
            PlayerController.camMove = true;
            playerRigid.isKinematic = false;  // 물리 효과를 영향줄지 말지? true 시 꺼짐
            playerRigid.gameObject.transform.position = tf_OriginPos.position;
            go_Stages[currentStage++].SetActive(false);
            go_Stages[currentStage].SetActive(true);
            go_UI.SetActive(false);

            theSM.ResetCurrentScore();
        }
        else
        {
            Debug.Log("모든 스테이지를 클리어 함.");
        }
    }
}
