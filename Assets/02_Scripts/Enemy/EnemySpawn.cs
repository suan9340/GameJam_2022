using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public bool enableSpawn = false;

    public GameObject enemyObj;

    public GameObject[] spawnPoint;

    public static readonly WaitForSeconds enemyDelay = new WaitForSeconds(2.5f);
    void Start()
    {
        enableSpawn = true;
        InvokeRepeating("SpawnEnemy", 3, 2.5f);

    }

    private IEnumerator ReadySpawnEnemy()
    {
        yield return new WaitForSeconds(2f);

        while(true)
        {

            yield return enemyDelay;
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(-3, 4);
        float randomY = Random.Range(-5, 6);

        float random1 = Random.Range(-3, -4);
        float random2 = Random.Range(3, 4);

        float randomx = Random.Range(-3, 4);
        float randomy = Random.Range(-6, 6);

        float random4 = Random.Range(-5, -6);
        float random6 = Random.Range(5, 6);

        if (randomX < 0 && randomY < 0)
        {
            GameObject enemy = Instantiate(enemyObj, new Vector3(randomx, random4, 0f), Quaternion.identity);
        }

        if (randomX > 0 && randomY > 0)
        {
            GameObject enemy = Instantiate(enemyObj, new Vector3(random2, randomy, 0f), Quaternion.identity);
        }

        if (randomX < 0 && randomY > 0)
        {
            GameObject enemy = Instantiate(enemyObj, new Vector3(random1, randomy, 0f), Quaternion.identity);
        }
        if (randomX > 0 && randomY < 0)
        {
            GameObject enemy = Instantiate(enemyObj, new Vector3(randomx, random6, 0f), Quaternion.identity);
        }
    }
}
