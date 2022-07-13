using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnerInfo
{
    public string name;
    public GameObject obj;
    public float outScore;
}

public class SpawnManager : MonoBehaviour
{
    #region SingleTon   

    private static SpawnManager _instance = null;
    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SpawnManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("SpawnManager").AddComponent<SpawnManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    public List<SpawnerInfo> enemyinfos = new List<SpawnerInfo>();


    public List<SpawnerInfo> iteminfos = new List<SpawnerInfo>();
    private Player_data playerData = null;

    public static readonly WaitForSeconds enemyDelay = new WaitForSeconds(1.8f);
    public static readonly WaitForSeconds enemy2Delay = new WaitForSeconds(3.5f);
    public static readonly WaitForSeconds enemy3Delay = new WaitForSeconds(5f);
    public static readonly WaitForSeconds itemDelay = new WaitForSeconds(20f);

    private Coroutine enemyCor;
    private Coroutine enemy2Cor;
    private Coroutine enemy3Cor;
    private Coroutine itemCor;

    void Start()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");

        enemyCor = StartCoroutine(ReadySpawnEnemy());
        enemy2Cor = StartCoroutine(ReadyEnemy2Spawn());
        enemy3Cor = StartCoroutine(ReadyEnemy3Spawn());

        itemCor = StartCoroutine(ReadyItemSpawn());
    }

    private IEnumerator ReadySpawnEnemy()
    {
        while (true)
        {
            if (GameManager.Instance.gameState == Game_State_Enum.isDie)
            {
                Debug.Log("적1 스폰은 그만!\n");
                StopCoroutine(enemyCor);

                yield break;
            }
            SpawnObject(enemyinfos[0].obj);
            yield return enemyDelay;
        }
    }


    private IEnumerator ReadyEnemy2Spawn()
    {
        while (true)
        {
            while (playerData.score < enemyinfos[1].outScore) yield return null;

            if (GameManager.Instance.gameState == Game_State_Enum.isDie)
            {
                Debug.Log("적2 스폰은 그만!\n");
                StopCoroutine(enemy2Cor);
            }

            Debug.Log("enemy2 스폰 시작");

            SpawnObject(enemyinfos[1].obj);

            yield return enemy3Delay;
        }
    }

    private IEnumerator ReadyEnemy3Spawn()
    {
        while (true)
        {
            while (playerData.score < enemyinfos[2].outScore) yield return null;

            if (GameManager.Instance.gameState == Game_State_Enum.isDie)
            {
                Debug.Log("적3 스폰은 그만!\n");
                StopCoroutine(enemy3Cor);
            }

            Debug.Log("enemy3 스폰 시작");

            SpawnObject(enemyinfos[2].obj);

            yield return enemy2Delay;
        }
    }

    private IEnumerator ReadyItemSpawn()
    {
        while (true)
        {
            while (playerData.score < iteminfos[0].outScore) yield return null;

            if (GameManager.Instance.gameState == Game_State_Enum.isDie)
            {
                Debug.Log("아이템 스폰은 그만!\n");
                StopCoroutine(itemCor);
            }

            Debug.Log("아이템 스폰 시작");

            SpawnObject(iteminfos[0].obj);

            yield return itemDelay;
        }
    }

    private void SpawnObject(GameObject _obj)
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
            var _enemy = Instantiate(_obj, new Vector3(randomx, random4, 0f), Quaternion.identity);
        }

        if (randomX > 0 && randomY > 0)
        {
            var _enemy = Instantiate(_obj, new Vector3(random2, randomy, 0f), Quaternion.identity);
        }

        if (randomX < 0 && randomY > 0)
        {
            var _enemy = Instantiate(_obj, new Vector3(random1, randomy, 0f), Quaternion.identity);
        }
        if (randomX > 0 && randomY < 0)
        {
            var _enemy = Instantiate(_obj, new Vector3(randomx, random6, 0f), Quaternion.identity);
        }
    }
}
