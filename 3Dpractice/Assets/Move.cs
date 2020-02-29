using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //Vector3 target = new Vector3(8, 1.5f, 0);
    //Vector3 velo = Vector3.up * 50;
    // Start is called before the first frame update
    void Start()
    {
       
      
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target, 2f);

        //transform.position = Vector3.SmoothDamp(transform.position, target, ref velo, 2f);

        //transform.position = Vector3.Lerp(transform.position, target, 0.05f);

        //transform.position = Vector3.Slerp(transform.position, target, 0.5f);
        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal")*Time.deltaTime, Input.GetAxisRaw("Vertical")*Time.deltaTime, 0);
        transform.Translate(vec);
     
    }
}
