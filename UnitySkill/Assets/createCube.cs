using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createCube : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateCoroutine());
    }

    IEnumerator CreateCoroutine()
    {
        while(true)
        {
            yield return null;
            GameObject t_object = ObjectPoolingManager.instance.GetQueue();
            t_object.transform.position = Vector3.zero;
        }
    }
}
