using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float regenTime = 3f;
    [SerializeField] GameObject[] enemyPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            int index = Random.Range(0, enemyPrefabs.Length);

            GameObject enemy = Instantiate(enemyPrefabs[index]);

            yield return new WaitForSeconds(regenTime);
        }
    }
}
