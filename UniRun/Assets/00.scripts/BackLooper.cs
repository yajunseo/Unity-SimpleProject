using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackLooper : MonoBehaviour
{
    float width;
    public float offsetX = -0.5f;
    public float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        width = collider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed);

        if(transform.position.x <= -width)
        {
            Vector3 setpos = new Vector3(width * 2f + offsetX, 0, 0);
            transform.position = transform.position + setpos;
        }
    }
}
