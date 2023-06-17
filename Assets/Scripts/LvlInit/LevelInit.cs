using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        [SerializeField] private SceneLoaderHandler _sceneLoaderHandler;
        [SerializeField] private EnemyDestroyingHandler _enemyDestroyingHandler;
        [SerializeField] private PlayerSelectedCharacter _playerSelectedCharacter;
        [SerializeField] private GameObject _distanceCanvas;
        [SerializeField] private LevelLoadMedaitor _levelLoadMedaitor;

        private readonly float _delayForGettingData = 1.5f;
        private List<EnemyObjectSpawner> _objectSpawners;
        private float _initialCameraPositionY;

        private void Awake()
        {
            _initialCameraPositionY = _camera.transform.position.y;
            _objectSpawners = _disablineGameObject.EnemyObjectSpawners;
            var position = _camera.transform.position;
            float targetPositionY = 150f;
            position = new Vector3(position.x, targetPositionY, position.z);
            _camera.transform.position = position;
            _cameraFollower.enabled = false;
            _deathCanvasEventView.enabled = false;
            _disablineGameObject.enabled = false;
            var offScreenIndicator = _distanceCanvas.GetComponentInChildren<OffScreenIndicator>();
            offScreenIndicator.SetCamera(_camera);
            SetDistanceCanvasActive(false);
            ConfigurePlayCanvas(false);

            SetPlayerForObjectSpawners();
        
            DataProvider.Instance.LoadInitialData();
        }

        private void Start()
        {
            StartCoroutine(nameof(InitializeDependencies));
        }

        private void OnEnable()
        {
            _playerSpawner.Spawned += Configure;
            DataProvider.Instance.LevelChanged += SetLevelForLevelMediator;
        }

        private void OnDisable()
        {
            DataProvider.Instance.LevelChanged -= SetLevelForLevelMediator;
            _playerSpawner.Spawned -= Configure;
        }

        private void SetDistanceCanvasActive(bool isActive)
        {
            _distanceCanvas.GetComponent<Canvas>().enabled = isActive;
        }

        private void SetPlayerForObjectSpawners()
        {
            foreach (var objectSpawner in _objectSpawners)
            {
                objectSpawner.SetPlayerSpawner(_playerSpawner);
            }
        }

        private void ConfigureCamera(float targetValue, float duration)
        {
            _camera.transform.DOMoveY(targetValue, duration).SetAutoKill(true);
        }

        private IEnumerator InitializeDependencies()
        {
            yield return new WaitForSeconds(_delayForGettingData);
            _levelLoadMedaitor.LoadLevel(DataProvider.Instance.GetLevel());            
            _playerSelectedCharacter.SetInitialCharacter(DataProvider.Instance.GetCharacter());
            _imageHandler.SetImage(DataProvider.Instance.GetImage());
            _audioVolumeHandler.SetVolume(DataProvider.Instance.GetVolume());
            _levelText.SetSceneIndex(DataProvider.Instance.GetLevel());
            Wallet.Instance.LoadMoney(DataProvider.Instance.GetMoney());
            _sceneLoaderHandler.SetLevel(DataProvider.Instance.GetLevel());
            _enemyDestroyingHandler.SetCount(DataProvider.Instance.GetEnemyCount());
            _playerSpawner.SetCharacterId(DataProvider.Instance.GetCharacter());
            yield return new WaitForSeconds(0.5f);
            ConfigureCamera(_initialCameraPositionY, 1.5f);
            yield return new WaitForSeconds(1.5f);
            SetDistanceCanvasActive(true);
            ConfigurePlayCanvas(true);
        }

        private void ConfigurePlayCanvas(bool isActive)
        {
            _playingCanvas.gameObject.SetActive(isActive);
            // _playingCanvas.enabled = isActive;
        }

        private void SetLevelForLevelMediator(int level) =>
            _levelLoadMedaitor.SetLevel(level);

        private void Configure(Player player)
        {
            GrapplingHook grapplingHook = player.GetComponentInChildren<GrapplingHook>();

            grapplingHook.SetCamera(_camera);
            _cameraFollower.enabled = true;
            _deathCanvasEventView.enabled = true;
            _disablineGameObject.enabled = true;

            print(player);

            //_healthBarState.enabled = true;

            //SetCamera();
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