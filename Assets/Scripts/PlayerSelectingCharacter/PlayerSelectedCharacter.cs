using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectedCharacter : MonoBehaviour
{
    [SerializeField] private List<GameObject> _characters = new List<GameObject>();
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _acceptButton;
    
    private int _currentIndex;
    private GoldMoneyPaySystem _paySystem;

    public IReadOnlyList<GameObject> Characters =>
        _characters;

    public event Action<int> CharacterChanged;

    private void Awake()
    {
        // DisableAllCharacters();
    }

    private void Start()
    {
        _paySystem = DependencyContainer.Get<GoldMoneyPaySystem>();
    }

    private void OnEnable()
    {
        _leftButton.onClick.AddListener(OnLeftButtonClicked);
        _rightButton.onClick.AddListener(OnRightButtonClicked);
        _acceptButton.onClick.AddListener(SaveChosenCharacter);
    }

    private void OnDisable()
    {
        _leftButton.onClick.RemoveListener(OnLeftButtonClicked);
        _rightButton.onClick.RemoveListener(OnRightButtonClicked);
        _acceptButton.onClick.RemoveListener(SaveChosenCharacter);
    }

    private void OnLeftButtonClicked()
    {
        _currentIndex--;

        if (_currentIndex < 0)
            _currentIndex = _characters.Count - 1;

        CharacterChanged?.Invoke(_currentIndex);

        UpdateCharacter();
    }

    private void OnRightButtonClicked()
    {
        _currentIndex++;

        if (_currentIndex >= _characters.Count)
            _currentIndex = 0;

        CharacterChanged?.Invoke(_currentIndex);

        UpdateCharacter();
    }

    public void SetInitialCharacter(int characterId)
    {
        DisableAllCharacters();
        _currentIndex = characterId;
        _characters[_currentIndex].SetActive(true);
        CharacterChanged?.Invoke(_currentIndex);
    }

    private void DisableAllCharacters()
    {
        foreach (var character in _characters)
        {
            character.SetActive(false);
        }
    }

    private void UpdateCharacter()
    {
        DisableAllCharacters();
        _characters[_currentIndex].SetActive(true);
    }

    private void SaveChosenCharacter()
    {
        if (TryBuyChosenCharacter(_currentIndex))
        {
            DisableAllCharacters();
            DataProvider.Instance.SaveCharacter(_currentIndex);
        }
    }

    private bool TryBuyChosenCharacter(int chosenPlayerId)
    {
        var characterId = _characters[chosenPlayerId].GetComponent<CharacterId>();

        if (DataProvider.Instance.IsPlayerPurchased(characterId.Index))
        {
            _paySystem.TryBuyCharacter(0);
            return true;
        }

        if (_paySystem.TryBuyCharacter(characterId.Price))
        {
            DataProvider.Instance.PurchaseCharacter(characterId.Index);
            return true;
        }

        return false;
    }
}

