using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    [SerializeField] float blinkSpeed;
    [SerializeField] int blickCount;

    [SerializeField] MeshRenderer mesh_PlayerGraphics;   // 점 선 면을 끌어주는 역할

    public void DecreaseHP(int _num)
    {
        SoundManager.instance.PlaySE("Hurt");
        StartCoroutine(BlinckCoroutune());
    }

    IEnumerator BlinckCoroutune()    // 코루틴 일종의 병렬 처리 기법, 대기 기능 구현이 간단하다
    {
        for(int i=0;i<blickCount * 2; i++)
        {
            mesh_PlayerGraphics.enabled = !mesh_PlayerGraphics.enabled;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}
