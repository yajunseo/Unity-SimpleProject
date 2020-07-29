using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    [SerializeField] int maxHP; // 최대 체력
    int currentHP; // 현제 체력

    [SerializeField] Text[] txt_HpArray; // 체력 UI

    bool isInvicibleMode = false; // 현제 무적 상태인지

    [SerializeField] float blinkSpeed;
    [SerializeField] int blickCount;

    [SerializeField] MeshRenderer mesh_PlayerGraphics;   // 점 선 면을 끌어주는 역할

    void Start()
    {
        currentHP = maxHP;
        UpdateHpStatus();
    }

    public void DecreaseHP(int _num)
    {
        if (!isInvicibleMode)
        {
            currentHP -= _num;
            UpdateHpStatus();
            if (currentHP <= 0)
                PlayerDead();

            SoundManager.instance.PlaySE("Hurt");
            StartCoroutine(BlinckCoroutune());
        }
    }

    public void IncreaseHp(int _num)
    {
        if (currentHP == maxHP)
            return;
            
        currentHP += _num;

        if (currentHP > maxHP)
            currentHP = maxHP;

        UpdateHpStatus();
    }


    IEnumerator BlinckCoroutune()    // 코루틴 일종의 병렬 처리 기법, 대기 기능 구현이 간단하다
    {
        isInvicibleMode = true;

        for(int i=0;i<blickCount * 2; i++)
        {
            mesh_PlayerGraphics.enabled = !mesh_PlayerGraphics.enabled;
            yield return new WaitForSeconds(blinkSpeed);
        }

        isInvicibleMode = false;
    }

    void UpdateHpStatus()
    {
        for(int i=0; i<txt_HpArray.Length;i++)
        {
            if (i < currentHP)
                txt_HpArray[i].gameObject.SetActive(true);
            else
                txt_HpArray[i].gameObject.SetActive(false);
        }
    }

    void PlayerDead()
    {
        Debug.Log("플레이어가 죽었습니다.");
    }
}
