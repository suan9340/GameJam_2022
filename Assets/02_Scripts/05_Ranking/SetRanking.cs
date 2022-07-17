using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerInfos
{
    public string bestName;
    public int bestScore;
}

public class SetRanking : MonoBehaviour
{
    [SerializeField] private List<PlayerInfos> players = new List<PlayerInfos>();

    private readonly string CurrentPlayerName = ConstantManager.RANK_PL_NAME;
    private readonly string CurrentPlayerScore = ConstantManager.RANK_PL_SCORE;

    private readonly string BestScore = ConstantManager.RANK_BESTSCORE;
    private readonly string BestName = ConstantManager.RANK_BESTNAME;
    public void ScoreSet(int _currentScore, string _currentName)
    {
        PlayerPrefs.SetString(CurrentPlayerName, _currentName);
        PlayerPrefs.SetInt(CurrentPlayerScore, _currentScore);

        var tmpScore = 0;
        var tmpName = "";

        for (int i = 0; i < players.Count; i++)
        {
            players[i].bestScore = PlayerPrefs.GetInt(i + BestScore);
            players[i].bestName = PlayerPrefs.GetString(i + BestName);

            // 만약 최고점수들보다 플레이어 점수가 더 크면?
            while (players[i].bestScore < _currentScore)
            {
                tmpScore = players[i].bestScore;
                tmpName = players[i].bestName;

                players[i].bestScore = _currentScore;
                players[i].bestName = _currentName;

                // 랭킹에 저장
                PlayerPrefs.SetInt(i + BestScore, _currentScore);
                PlayerPrefs.SetString(i + BestName, _currentName);

                _currentScore = tmpScore;
                _currentName = tmpName;
            }
        }

        for (int i = 0; i < players.Count; i++)
        {
            PlayerPrefs.SetInt(i + BestScore, players[i].bestScore);
            PlayerPrefs.SetString(i.ToString() + BestName, players[i].bestName);
        }
    }

    public void ShowRanking()
    {

    }
}
