using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Player_data playerData = null;

    public GameObject enemyObj;
    public GameObject bulletitemObj;

    public static readonly WaitForSeconds enemyDelay = new WaitForSeconds(1.6f);
    public static readonly WaitForSeconds itemDelay = new WaitForSeconds(10f);


    private Coroutine enemyCor;
    private Coroutine itemCor;

    private bool isA = false;

    void Start()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");

        enemyCor = StartCoroutine(ReadySpawnEnemy());
        itemCor = StartCoroutine(ReadyItemSpawn());
    }

    private IEnumerator ReadySpawnEnemy()
    {
        while (true)
        {
            if (GameManager.Instance.gameState == Game_State_Enum.isDie)
            {
                Debug.Log("적 스폰은 그만!\n");
                StopCoroutine(enemyCor);

                yield break;
            }
            SpawnObject(enemyObj);
            yield return enemyDelay;
        }
    }

    private IEnumerator ReadyItemSpawn()
    {
        while (true)
        {
            while (playerData.score < 500) yield return null;

            if (GameManager.Instance.gameState == Game_State_Enum.isDie)
            {
                Debug.Log("아이템 스폰은 그만!\n");
                StopCoroutine(itemCor);
            }
            Debug.Log("아이템 스폰 시작");

            SpawnObject(bulletitemObj);

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
