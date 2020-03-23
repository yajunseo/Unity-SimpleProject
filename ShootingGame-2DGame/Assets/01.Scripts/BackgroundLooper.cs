using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    float height;

    [SerializeField] float speed = 5f;
    //SerializeField는 Private 변수가 인스펙터창에 나타날 수 있도록 해주는 역할을 한다.

    [SerializeField] float offsetY = 1f;

    Vector2 setPos;
          
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        height = collider.bounds.size.y;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);

        if(transform.position.y <= -height)
        {
            //재위치 시키기
            setPos.x = transform.position.x;
            setPos.y = transform.position.y + height * 2f - offsetY;

            transform.position = setPos;

        }

    }


}
