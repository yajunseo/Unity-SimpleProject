using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMove : EnemyMove
{
    [SerializeField] GameObject pattern;

    Vector3[] wayPoints;

    int wayIndex;


    // Start is called before the first frame update
    void Start()
    {
        //pattern = GameObject.Find("Pattern1");

        int count = pattern.transform.childCount;

        wayPoints = new Vector3[count];

        for(int i=0; i< count; i++)
        {
            wayPoints[i] = pattern.transform.GetChild(i).position;

        }

        base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        if (isAwake == false)
        {
            AwakeMove();
        }
        else
        {
            //다음 지점과의 거리가 0.1 보다 작을때(거의 도착했을때)
            if (Vector2.Distance(transform.position, wayPoints[wayIndex]) < 0.1f)
            {
                wayIndex++;

                if(wayIndex >= wayPoints.Length)
                {
                    wayIndex = 0;
                }

            }

            Vector2 direction = wayPoints[wayIndex] - transform.position;

            moveVec = direction.normalized;

        }

        transform.Translate(moveVec * speed * Time.deltaTime, Space.World);

    }
}
