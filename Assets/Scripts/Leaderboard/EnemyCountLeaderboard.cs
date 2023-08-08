using Agava.YandexGames;
using System;
using System.Collections.Generic;
using I2.Loc;
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

    private Dictionary<string, string> _anonymousTexts = new()
    {
        { "ru", "Анонимный" },
        { "en", "Anonymous" },
        { "tr", "Anonim" }
    };

    private Dictionary<string, string> _anonymousCurrentLangTexts = new()
    {
        { "Russian", "Анонимный" },
        { "English", "Anonymous" },
        { "Turkish", "Anonim" }
    };

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        _openLeaderboard.onClick.AddListener(OnEnableLeaderboard);
    }

    private void OnDisable()
    {
        _openLeaderboard.onClick.RemoveListener(OnEnableLeaderboard);
    }

    public void LoadLeaderboard()
    {
        if (PlayerAccount.IsAuthorized)
        {
            Init();
        }
    }

    private void Init() =>
        Leaderboard.GetEntries(Name, (result) =>
        {
            Debug.Log($"My rank = {result.userRank}");
            FillArray(result);
        });


    private void OnEnableLeaderboard()
    {
        if (PlayerAccount.IsAuthorized)
        {
            PlayerAccount.RequestPersonalProfileDataPermission();

            if (PlayerAccount.HasPersonalProfileDataPermission == false)
                return;

            Init();

            DataLoaded?.Invoke();
            return;
        }

        PlayerAccount.Authorize();

        Init();
        DataLoaded?.Invoke();
    }

    private void FillArray(LeaderboardGetEntriesResponse result)
    {
        for (int i = 0; i < result.entries.Length; i++)
        {
            _names[i].text = result.entries[i].player.publicName;

            if (string.IsNullOrEmpty(_names[i].text))
            {
                name = _anonymousCurrentLangTexts[LocalizationManager.CurrentLanguage];
                name = _anonymousTexts[YandexGamesSdk.Environment.i18n.lang];
                _names[i].text = name;
            }

            _ranks[i].text = result.entries[i].rank.ToString();
            _scores[i].text = result.entries[i].score.ToString();
        }
    }
}