using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public static class CanvasExtension
{
    public static void MoveCanvasAlpha(this CanvasGroup canvas, float targetAlphaValue, float duration)
    {
        canvas.DOFade(targetAlphaValue, duration).SetUpdate(true);
    }
}