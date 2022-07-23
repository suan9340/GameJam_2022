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

    private readonly WaitForSeconds enemyDelay = new WaitForSeconds(1.3f);
    private readonly WaitForSeconds enemy2Delay = new WaitForSeconds(10f);
    private readonly WaitForSeconds enemy3Delay = new WaitForSeconds(8f);
    private readonly WaitForSeconds enemy4Delay = new WaitForSeconds(10f);

    private readonly WaitForSeconds itemDelay = new WaitForSeconds(40f);
    private readonly WaitForSeconds item2Delay = new WaitForSeconds(50f);

    private Coroutine enemyCor;
    private Coroutine enemy2Cor;
    private Coroutine enemy3Cor;
    private Coroutine enemy4Cor;

    private Coroutine itemCor;
    private Coroutine item2Cor;

    void Start()
    {
        playerData = Resources.Load<Player_data>("SO/" + "PlayerData");

        enemyCor = StartCoroutine(ReadySpawnEnemy());
        enemy2Cor = StartCoroutine(ReadyEnemy2Spawn());
        enemy3Cor = StartCoroutine(ReadyEnemy3Spawn());
        enemy4Cor = StartCoroutine(ReadyEnemy4Spawn());

        itemCor = StartCoroutine(ReadyItemSpawn());
        item2Cor = StartCoroutine(ReadyItem2Spawn());
    }

    private IEnumerator ReadySpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);

            while (GameManager.Instance.gameState == Game_State_Enum.isSetting) yield return null;

            if (GameManager.Instance.gameState == Game_State_Enum.isDie)
            {
                Debug.Log("��1 ������ �׸�!\n");
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
            while (GameManager.Instance.gameState == Game_State_Enum.isSetting) yield return null;

            while (playerData.score < enemyinfos[1].outScore) yield return null;

            if (GameManager.Instance.gameState == Game_State_Enum.isDie)
            {
                Debug.Log("��2 ������ �׸�!\n");
                StopCoroutine(enemy2Cor);
            }

            Debug.Log("enemy2 ���� ����");

            SpawnObject(enemyinfos[1].obj);

            yield return enemy3Delay;
        }
    }

    private IEnumerator ReadyEnemy3Spawn()
    {
        while (true)
        {
            while (GameManager.Instance.gameState == Game_State_Enum.isSetting) yield return null;

            while (playerData.score < enemyinfos[2].outScore) yield return null;

            if (GameManager.Instance.gameState == Game_State_Enum.isDie)
            {
                Debug.Log("��3 ������ �׸�!\n");
                StopCoroutine(enemy3Cor);

                yield break;
            }

            Debug.Log("enemy3 ���� ����");

            SpawnObject(enemyinfos[2].obj);

            yield return enemy2Delay;
        }
    }

    private IEnumerator ReadyEnemy4Spawn()
    {
        while (true)
        {
            while (GameManager.Instance.gameState == Game_State_Enum.isSetting) yield return null;

            while (playerData.score < enemyinfos[3].outScore) yield return null;

            if (GameManager.Instance.gameState == Game_State_Enum.isDie)
            {
                Debug.Log("��4 ������ �׸�!\n");
                StopCoroutine(enemy4Cor);

                yield break;
            };

            Debug.Log("enemy4 ���� ����");

            SpawnObject(enemyinfos[3].obj);

            yield return enemy4Delay;
        }
    }
    private IEnumerator ReadyItemSpawn()
    {
        while (true)
        {
            while (GameManager.Instance.gameState == Game_State_Enum.isSetting) yield return null;

            while (playerData.score < iteminfos[0].outScore) yield return null;

            if (GameManager.Instance.gameState == Game_State_Enum.isDie)
            {
                Debug.Log("������ ������ �׸�!\n");
                StopCoroutine(itemCor);
            }

            Debug.Log("������ ���� ����");

            SpawnObject(iteminfos[0].obj);

            yield return itemDelay;
        }
    }

    private IEnumerator ReadyItem2Spawn()
    {
        while (true)
        {
            while (GameManager.Instance.gameState == Game_State_Enum.isSetting) yield return null;

            //while ((playerData.score < iteminfos[1].outScore) && (playerData.current_attackPower > (playerData.max_attackPower / 2) + 5)) yield return null;

            while (playerData.score < iteminfos[1].outScore) yield return null;
            while (playerData.current_attackPower > playerData.max_attackPower / 2) yield return null;

            if (GameManager.Instance.gameState == Game_State_Enum.isDie)
            {
                Debug.Log("�� ������ ������ �׸�!\n");
                StopCoroutine(item2Cor);
            }

            Debug.Log("�� ������ ���� ����");

            SpawnObject(iteminfos[1].obj);

            yield return item2Delay;
        }
    }


    private void SpawnObject(GameObject _obj)
    {
        float randomX = Random.Range(-12, 12);
        float randomY = Random.Range(-7, -7);

        float random1 = Random.Range(-12, -10);
        float random2 = Random.Range(10, 12);

        float randomx = Random.Range(-11, 11);
        float randomy = Random.Range(-6, 6);

        float random4 = Random.Range(-6, -8);
        float random6 = Random.Range(6, 8);

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
