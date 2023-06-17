using UnityEngine;

public static class SaveService
{
    private const string Money = "Money";
    private const string Image = "Image";

    public static void SetMoney(int money)
    {
        //PlayerAccount.SetCloudSaveData(Money);
        PlayerPrefs.SetInt(Money, money);
        PlayerPrefs.Save();
    }

    public static int GetMoney() =>
    //PlayerAccount.GetProfileData(PlayerAccount.SetCloudSaveData(Money));
    PlayerPrefs.GetInt(Money);

    public static void SetChoosedImage(int image)
    {
        PlayerPrefs.SetInt(Image, image);
        PlayerPrefs.Save();
    }

    public static int GetChoosedImage() =>
        PlayerPrefs.GetInt(Image);
}