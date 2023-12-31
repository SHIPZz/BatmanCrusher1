using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterPriceShowerHandler : MonoBehaviour
{
    [SerializeField] private GameObject _wolverinePricePrefab;
    [SerializeField] private GameObject _spiderPricePrefab;
    [SerializeField] private PlayerSelectedCharacter _selectedCharacter;

    private List<GameObject> _characterPrices = new List<GameObject>();
    private TextMeshProUGUI _moneySpiderText;
    private TextMeshProUGUI _moneyWolverineText;
    private IReadOnlyList<GameObject> _characters;
    private CharacterId _lastPlayerId;

    private void Start()
    {
        _characters = _selectedCharacter.Characters;
        _moneyWolverineText = _wolverinePricePrefab.GetComponentInChildren<TextMeshProUGUI>();
        _moneySpiderText = _spiderPricePrefab.GetComponentInChildren<TextMeshProUGUI>();

        _moneySpiderText.text = Constant.SpiderPrice.ToString();
        _moneyWolverineText.text = Constant.WolverinePrice.ToString();

        _characterPrices.Add(_wolverinePricePrefab);
        _characterPrices.Add(_spiderPricePrefab);

        _selectedCharacter.CharacterChanged += OnChosenCharacter;
    }

    private void OnDisable()
    {
        _selectedCharacter.CharacterChanged -= OnChosenCharacter;
    }

    private void OnChosenCharacter(int chosenPlayerId)
    {
        if (_lastPlayerId is not null)
            SetActivePrice(_lastPlayerId.PricePrefab, false);
        
        var characterId = _characters[chosenPlayerId].GetComponent<CharacterId>();

        if (DataProvider.Instance.IsPlayerPurchased(characterId.Index))
        {
            SetActivePrice(characterId.PricePrefab, false);
            return;
        }

        _lastPlayerId = characterId;
        SetActivePrice(characterId.PricePrefab, true);
    }

    private void SetActivePrice(GameObject price, bool isActive) =>
        price.SetActive(isActive);

    private void SetActive(bool isEnabled) =>
        _characterPrices.ForEach(x => x.SetActive(isEnabled));
}