using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMenuCanvasState : MonoBehaviour, IButtonCanvas, IButtonCanvasState
{
    [SerializeField] private Button _closingButton;
    [SerializeField] private Button _closingNotEnoughMoneyButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _resumptionButton;

    private void OnEnable()
    {
        _closingButton.onClick.AddListener(EnableButtons);
        _closingNotEnoughMoneyButton.onClick.AddListener(EnableButtons);
    }

    private void OnDisable()
    {
        _closingButton.onClick.RemoveListener(EnableButtons);
        _closingNotEnoughMoneyButton.onClick.RemoveListener(EnableButtons);
    }

    public void DisableButtons()
    {
        ControlButtonsInteraction(false);
    }

    public void EnableButtons()
    {
        ControlButtonsInteraction(true);
    }

    public void ControlButtonsInteraction(bool isActive)
    {
        _homeButton.interactable = isActive;
        _resumptionButton.interactable = isActive;
    }
}
