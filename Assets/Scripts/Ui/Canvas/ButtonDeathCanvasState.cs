using UnityEngine;
using UnityEngine.UI;

public class ButtonDeathCanvasState : MonoBehaviour, IButtonCanvas, IButtonCanvasState
{
    [SerializeField] private Button _advertisingButton;
    [SerializeField] private Button _closingButton;
    [SerializeField] private CanvasGroupAlphaHandler _canvasAlphaState;

    private void OnEnable()
    {
        //_canvasAlphaState.NotEnoughMoneyCanvasTurned += DisableButtons;
        //_closingButton.onClick.AddListener(EnableButtons);
    }

    private void OnDisable()
    {
        //_canvasAlphaState.NotEnoughMoneyCanvasTurned -= DisableButtons;
        //_closingButton.onClick.RemoveListener(EnableButtons);
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
        _advertisingButton.interactable = isActive;
    }
}