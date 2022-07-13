using System.Collections.Generic;
using UnityEngine;

// 파티클을 관리한다 
// 원하는 위치에 만들어주고, 해당 파티클을 강제로 삭제도 해준다!
// - 편의 기능
//    싱글톤, 파티클 프리펩 등을 갖고 있다!
public class ParticleManager : MonoBehaviour
{
    #region SingleTon

    private static ParticleManager _instance = null;
    public static ParticleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ParticleManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("ParticleManager").AddComponent<ParticleManager>();
                }
            }
            return _instance;
        }
    }

    #endregion
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    #region 인터페이스

    public enum ParticleType
    {
        enemyHit,
        enmeyDie,
        itemEat,
        playerDie,
    }

    public int AddParticle(ParticleType pt, Vector3 pos)
    {
        switch (pt)
        {
            case ParticleType.enemyHit:
                if (false == particleDic.ContainsKey(pt))
                {
                    particleDic[pt] = Resources.Load<GameObject>("VFX/enemyHit");
                }
                break;

            case ParticleType.enmeyDie:
                if (false == particleDic.ContainsKey(pt))
                {
                    particleDic[pt] = Resources.Load<GameObject>("VFX/enemyDead");
                }
                break;

            case ParticleType.itemEat:
                if (false == particleDic.ContainsKey(pt))
                {
                    particleDic[pt] = Resources.Load<GameObject>("VFX/ItemEat");
                }
                break;

            case ParticleType.playerDie:
                if (false == particleDic.ContainsKey(pt))
                {
                    particleDic[pt] = Resources.Load<GameObject>("VFX/playerDead");
                }
                break;

            default:
                Debug.LogWarning("연결하지 않은 파티클이 있다고!?!?!");
                return 0;
        }

        if (particleDic[pt] == null)
        {
            Debug.LogWarning($"로딩을 못했엉 {pt}");
            return 0;
        }

        GameObject go = Instantiate<GameObject>(particleDic[pt], pos, Quaternion.identity);

        return 0;
    }

    private static Dictionary<ParticleType, GameObject> particleDic = new Dictionary<ParticleType, GameObject>();
    #endregion
}