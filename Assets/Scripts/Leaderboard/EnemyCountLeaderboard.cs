using Agava.YandexGames;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCountLeaderboard : MonoBehaviour
{
    private const string Name = "EnemyCountLeaderboard";

    [SerializeField] private TextMeshProUGUI[] _names;
    [SerializeField] private TextMeshProUGUI[] _scores;
    [SerializeField] private TextMeshProUGUI[] _ranks;
    [SerializeField] private Button _openLeaderboard;

    public event Action DataLoaded;

    public event Action DataNotLoaded;

    private void OnEnable()
    {
        _openLeaderboard.onClick.AddListener(OnEnableLeaderboard);
    }

    private void OnDisable()
    {
        _openLeaderboard.onClick.RemoveListener(OnEnableLeaderboard);
    }

    public void OnEnableLeaderboard()
    {
        if (PlayerAccount.IsAuthorized)
        {
            if (PlayerAccount.HasPersonalProfileDataPermission == false)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();

                if (PlayerAccount.HasPersonalProfileDataPermission == false)
                    DataNotLoaded?.Invoke();

                return;
            }

            Leaderboard.GetEntries(Name, (result) =>
            {
                Debug.Log($"My rank = {result.userRank}");
                FillArray(result);
            });

            DataLoaded?.Invoke();
            return;
        }

        PlayerAccount.Authorize();
    }

    private void FillArray(LeaderboardGetEntriesResponse result)
    {
        for (int i = 0; i < result.entries.Length; i++)
        {
            _names[i].text = result.entries[i].player.publicName;

            if (string.IsNullOrEmpty(_names[i].text))
                name = "Anonymous";

            _ranks[i].text = result.entries[i].rank.ToString();
            _scores[i].text = result.entries[i].score.ToString();

            //string name = result.entries[i].player.ToString();

        }
    }
}