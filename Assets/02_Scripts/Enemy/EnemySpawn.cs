using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public bool enableSpawn = false;

    public GameObject Enemy;
    void Start()
    {
        enableSpawn = true;
        InvokeRepeating("SpawnEnemy", 3, 1); 
        
    }

    void Update()
    {

    }

  

    void SpawnEnemy()
    {
        float randomX = Random.Range(-2.5f, 2.5f);
        float randomY = Random.Range(-5f, 5f);
        if (enableSpawn)
        {
            GameObject enemy = (GameObject)Instantiate(Enemy, new Vector3(randomX, randomY, 0f), Quaternion.identity); 
        }
    }

  
}
