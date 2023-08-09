using System.Collections;
using System.Collections.Generic;
using Agava.WebUtility;
using Agava.YandexGames;
using DG.Tweening;
using I2.Loc;
using UnityEngine;

namespace LvlInit
{
    public class LevelInit : MonoBehaviour
    {
        [SerializeField] private CameraFollower _cameraFollower;
        [SerializeField] private DeathCanvasEventView _deathCanvasEventView;
        [SerializeField] private DisablingGameObjectEvent _disablineGameObject;
        [SerializeField] private PlayingCanvas _playingCanvas;
        [SerializeField] private CanvasGroupAlphaHandler _canvasAlphaState;
        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private ImageHandler _imageHandler;
        [SerializeField] private AudioVolumeHandler _audioVolumeHandler;
        [SerializeField] private LevelText _levelText;
        [SerializeField] private EnemyDestroyingHandler _enemyDestroyingHandler;
        [SerializeField] private PlayerSelectedCharacter _playerSelectedCharacter;
        [SerializeField] private GameObject _distanceCanvas;
        [SerializeField] private LevelLoadMedaitor _levelLoadMedaitor;
        [SerializeField] private PlayingAdvertisingHandler _playingAdvertisingHandler;
        [SerializeField] private EnemyCountLeaderboard _enemyCountLeaderboard;
        [SerializeField] private WalletUIView _walletUIView;

        private readonly float _delayForGettingData = 2.4f;
        private List<EnemyObjectSpawner> _objectSpawners;
        private float _initialCameraPositionY;
        private HealthRecoveryEvent _healthRecoveryEvent;
        private bool _isClosed = false;

        private Dictionary<string, string> _languages = new()
        {
            { "ru", "Russian" },
            { "en", "English" },
            { "tr", "Turkish" }
        };

        private void Awake()
        {
            _canvasAlphaState.SetPlayingAdvertisingHandler(_playingAdvertisingHandler);
            _audioVolumeHandler.SetPlayingAdvertisingHandler(_playingAdvertisingHandler);
            _initialCameraPositionY = _camera.transform.position.y;
            _objectSpawners = _disablineGameObject.EnemyObjectSpawners;
            var position = _camera.transform.position;
            float targetPositionY = 400f;
            position = new Vector3(position.x, targetPositionY, position.z);
            _camera.transform.position = position;
            _cameraFollower.enabled = false;
            _deathCanvasEventView.enabled = false;
            _disablineGameObject.enabled = false;
            AudioListener.pause = false;
            var offScreenIndicator = _distanceCanvas.GetComponentInChildren<OffScreenIndicator>();
            offScreenIndicator.SetCamera(_camera);
            SetDistanceCanvasActive(false);
            ConfigurePlayCanvas(false);
            YandexGamesSdk.CallbackLogging = false;

            SetPlayerForObjectSpawners();
        }

        private void Start()
        {
            StartCoroutine(nameof(InitializeDependencies));
        }

        private void OnEnable()
        {
            _playerSpawner.Spawned += Configure;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
            Application.focusChanged += OnFocusChanged;
        }

        private void OnDisable()
        {
            _playerSpawner.Spawned -= Configure;
            Application.focusChanged -= OnFocusChanged;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        }

        private void OnFocusChanged(bool isChanged)
        {
            if (!isChanged)
            {
                OnInBackgroundChange(true);
                return;
            }

            OnInBackgroundChange(false);
        }

        private void OnInBackgroundChange(bool inBackground)
        {
            AudioListener.pause = inBackground;

            Time.timeScale = inBackground ? 0f : 1f;
        }

        public void UploadData()
        {
            _imageHandler.SetImage(DataProvider.Instance.GetImage());
            _audioVolumeHandler.SetVolume(DataProvider.Instance.GetVolume());
            _enemyDestroyingHandler.SetCount(DataProvider.Instance.GetEnemyCount());
            _playerSpawner.SetCharacterId(DataProvider.Instance.GetCharacter());
            _playerSelectedCharacter.SetInitialCharacter(DataProvider.Instance.GetCharacter());
            _enemyCountLeaderboard.LoadLeaderboard();
            Wallet.Instance.LoadMoney(DataProvider.Instance.GetMoney());
            _walletUIView.SetMoneyCount(DataProvider.Instance.GetMoney());
            DOTween.Sequence().AppendInterval(2.5f);
            SetDistanceCanvasActive(true);
            ConfigurePlayCanvas(true);
        }

        private IEnumerator InitializeDependencies()
        {
            yield return new WaitForSeconds(0.8f);
            _playerSpawner.SetCharacterId(DataProvider.Instance.GetCharacter());
            _playerSelectedCharacter.SetInitialCharacter(DataProvider.Instance.GetCharacter());
            _enemyDestroyingHandler.SetCount(DataProvider.Instance.GetEnemyCount());
            ConfigureCamera(_initialCameraPositionY, 1.5f);
            yield return new WaitForSeconds(1.5f);
            _audioVolumeHandler.SetVolume(DataProvider.Instance.GetVolume());
            _enemyCountLeaderboard.LoadLeaderboard();
            LocalizationManager.CurrentLanguage = _languages[YandexGamesSdk.Environment.i18n.lang];
            Wallet.Instance.LoadMoney(DataProvider.Instance.GetMoney());
            _walletUIView.SetMoneyCount(DataProvider.Instance.GetMoney());
            _imageHandler.SetImage(YandexGamesSdk.Environment.i18n.lang);
            SetDistanceCanvasActive(true);
            ConfigurePlayCanvas(true);
        }

        private void SetDistanceCanvasActive(bool isActive)
        {
            _distanceCanvas.GetComponent<Canvas>().enabled = isActive;
        }

        private void SetPlayerForObjectSpawners()
        {
            foreach (EnemyObjectSpawner objectSpawner in _objectSpawners)
            {
                objectSpawner.SetPlayerSpawner(_playerSpawner);
            }
        }

        private void ConfigureCamera(float targetValue, float duration) =>
            _camera.transform.DOMoveY(targetValue, duration).SetAutoKill(true);

        private void ConfigurePlayCanvas(bool isActive) =>
            _playingCanvas.gameObject.SetActive(isActive);

        private void Configure(Player player)
        {
            GrapplingHook grapplingHook = player.GetComponentInChildren<GrapplingHook>();

            grapplingHook.SetCamera(_camera);
            _cameraFollower.enabled = true;
            _deathCanvasEventView.enabled = true;
            _disablineGameObject.enabled = true;

            _healthRecoveryEvent = player.GetComponentInChildren<HealthRecoveryEvent>();
            _healthRecoveryEvent.SetPlayingAdvertisingHandler(_playingAdvertisingHandler);

            SetPlayer(player);
        }

        private void SetPlayer(Player player)
        {
            _deathCanvasEventView.SetPlayer(player);
            _cameraFollower.SetPlayer(player);
            _disablineGameObject.SetPlayer(player);
        }
    }
}