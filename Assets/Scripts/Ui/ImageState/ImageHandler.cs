using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class ImageHandler : MonoBehaviour
{
    [SerializeField] private GameObject _russianTick;
    [SerializeField] private GameObject _englishTick;
    [SerializeField] private GameObject _turkishTick;

    [SerializeField] private Button _englishLanguage;
    [SerializeField] private Button _russianLanguage;
    [SerializeField] private Button _turkishLanguage;

    private readonly int _englishImageId = 1;
    private readonly int _russianImageId = 2;
    private readonly int _turkishImageId = 3;

    public int ChoosedImageId { get; private set; }

    private void Start()
    {
        SetInitialValueScale();

        // SetImage(DataProvider.Instance.GetImage());

        if (LocalizationManager.CurrentLanguage == "English")
            _englishTick.ChangeScale(1, 0);

        if (LocalizationManager.CurrentLanguage == "Russian")
            _russianTick.ChangeScale(1, 0);

        if (LocalizationManager.CurrentLanguage == "Turkish")
            _turkishTick.ChangeScale(1, 0);
    }

    private void OnEnable()
    {
        _englishLanguage.onClick.AddListener(OnEnglishLanguageChoosed);
        _russianLanguage.onClick.AddListener(OnRussianLanguageChoosed);
        _turkishLanguage.onClick.AddListener(OnTurkishLanguageChoosed);
    }

    private void OnDisable()
    {
        _englishLanguage.onClick.RemoveListener(OnEnglishLanguageChoosed);
        _russianLanguage.onClick.RemoveListener(OnRussianLanguageChoosed);
        _turkishLanguage.onClick.RemoveListener(OnTurkishLanguageChoosed);
    }

    public void SetImage(int imageId)
    {
        ChoosedImageId = imageId;
    }

    private void SetInitialValueScale()
    {
        _turkishTick.ChangeScale(0, 0);
        _russianTick.ChangeScale(0, 0);
        _englishTick.ChangeScale(0, 0);
    }

    private void OnEnglishLanguageChoosed()
    {
        _turkishTick.ChangeScale(0, 1);
        _russianTick.ChangeScale(0, 1);
        _englishTick.ChangeScale(1, 1);

        ChoosedImageId = _englishImageId;
        DataProvider.Instance.SaveImage(ChoosedImageId);
    }

    private void OnTurkishLanguageChoosed()
    {
        _englishTick.ChangeScale(0, 1);
        _russianTick.ChangeScale(0, 1);
        _turkishTick.ChangeScale(1, 1);

        ChoosedImageId = _turkishImageId;
        print(ChoosedImageId);
        DataProvider.Instance.SaveImage(ChoosedImageId);
    }

    private void OnRussianLanguageChoosed()
    {
        _turkishTick.ChangeScale(0, 1);
        _englishTick.ChangeScale(0, 1);
        _russianTick.ChangeScale(1, 1);

        ChoosedImageId = _russianImageId;
        DataProvider.Instance.SaveImage(ChoosedImageId);
    }
}