using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    [SerializeField] Transform m_tfArrow = null;

    Camera m_cam = null;

    // Start is called before the first frame update
    void Start()
    {
        m_cam = Camera.main;
    }
    void LookatMouse()
    {
        Vector2 t_mousePos = m_cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 t_direction = new Vector2(t_mousePos.x - m_tfArrow.position.x,
                                            t_mousePos.y - m_tfArrow.position.y);

        m_tfArrow.right = t_direction;
    }    

    void TryFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject temp = objectpoolingManager.instance.GetQueue();
            temp.transform.position = m_tfArrow.position;
            temp.transform.rotation = m_tfArrow.rotation;
            temp.transform.right = m_tfArrow.right;
            temp.GetComponent<Rigidbody2D>().velocity = m_tfArrow.transform.right * 10f;

            StartCoroutine(DestroyArrow(temp));
        }
    }


    // Update is called once per frame
    void Update()
    {
        LookatMouse();
        TryFire();
    }

    IEnumerator DestroyArrow(GameObject gameobject)
    {
        yield return new WaitForSeconds(2f);
        objectpoolingManager.instance.InsertQueue(gameobject);
    }
}
