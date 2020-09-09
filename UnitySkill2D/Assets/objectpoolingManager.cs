using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectpoolingManager : MonoBehaviour
{
    public static objectpoolingManager instance;
    public Queue<GameObject> m_queue = new Queue<GameObject>();
    [SerializeField] GameObject goProfab = null;
    [SerializeField] Transform m_tfArrow = null;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        for(int i=0;i<50;i++)
        {
            GameObject temp = Instantiate(goProfab, m_tfArrow.position, Quaternion.identity);
            temp.SetActive(false);
            m_queue.Enqueue(temp);
        }
    }

    public void InsertQueue(GameObject p_object)
    {
        p_object.SetActive(false);
        m_queue.Enqueue(p_object);
    }

    public GameObject GetQueue()
    {
        GameObject temp = m_queue.Dequeue();
        temp.SetActive(true);
        return temp;
    }
}
