using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class TextColorExtension
{
    private static System.Random _random = new();

    public static void Change(this TextMeshProUGUI text)
    {
        List<Color> colors = new List<Color>();

        FillList(colors);

        int randomValue = GetRandomValue(colors);

        text.DOColor(colors[randomValue], 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    private static void FillList(List<Color> colors)
    {
        colors.Add(Color.red);
        colors.Add(Color.green);
        colors.Add(Color.blue);
        colors.Add(Color.white);
        colors.Add(Color.yellow);
    }

    private static int GetRandomValue(List<Color> list) =>
        _random.Next(0, list.Count);
}