using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimator : MonoBehaviour
{
    [SerializeField] Sprite[] sprites; //변경할 이미지들

    int maxFrameNum;

    [SerializeField] float aniSpeed; //이미지를 변경하는 속도

    float curTime;

    int frameIndex;

    [SerializeField] bool isLoop; //애니메이션을 반복할지에 대한 정보

    SpriteRenderer renderer;
    

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        maxFrameNum = sprites.Length;

    }

    // Update is called once per frame
    void Update()
    {
        curTime += aniSpeed * Time.deltaTime;

        if(curTime > 1f)
        {
            curTime = 0;

            if(isLoop == true) //반복하는 애니메이션일때
            {
                renderer.sprite = sprites[frameIndex];

                frameIndex++;

                if(frameIndex >= maxFrameNum)
                {
                    frameIndex = 0;
                }

            }
            else //애니메이션 진행이 다되면 소멸하는 오브젝트일때
            {
                if(frameIndex < maxFrameNum)
                {
                    renderer.sprite = sprites[frameIndex];

                }

                frameIndex++;

                if(frameIndex >= maxFrameNum + 1)
                {
                    Destroy(gameObject);
                }


            }

        }



    }
}
