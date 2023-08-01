using UnityEngine;
using UnityEngine.UI;

public class VictoryButtonCanvasState : MonoBehaviour
{
    [SerializeField] private PlayingAdvertisingHandler _playingAdvert;
    [SerializeField] private Button _advertisingButton;
    [SerializeField] private Button _claimButton;
    [SerializeField] private CanvasGroupAlphaHandler _canvsaGroupAlphaState;

    private void OnEnable()
    {
        _advertisingButton.onClick.AddListener(DisableButtons);
        _canvsaGroupAlphaState.PlayCanvasDisabled += TurnButtons;
    }

    private void OnDisable()
    {
        _advertisingButton.onClick.RemoveListener(DisableButtons);
        _canvsaGroupAlphaState.PlayCanvasDisabled -= TurnButtons;
    }

    private void TurnButtons()
    {
        ControlButtonState(true);
    }

    private void DisableButtons()
    {
        ControlButtonState(false);
    }

    private void ControlButtonState(bool isActive)
    {
        _advertisingButton.interactable = isActive;
        _claimButton.interactable = isActive;
    }
}