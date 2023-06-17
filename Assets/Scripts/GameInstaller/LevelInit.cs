using System.Collections.Generic;
using UnityEngine;

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

    private List<EnemyObjectSpawner> _objectSpawners;

    private GameFactory _gameFactory;

    private void Awake()
    {
        _objectSpawners = _disablineGameObject.EnemyObjectSpawners;
        _cameraFollower.enabled = false;
        _deathCanvasEventView.enabled = false;
        _disablineGameObject.enabled = false;

        foreach (var objectSpawner in _objectSpawners)
        {
            objectSpawner.SetPlayerSpawner(_playerSpawner);
        }
        
        DataProvider.Instance.LoadInitialData();
    }

    private void Start()
    {
        InitializeDependencies();
        _gameFactory = DependencyContainer.Get<GameFactory>();
    }

    private void OnEnable()
    {
        _playerSpawner.Spawned += Configure;
    }

    private void OnDisable()
    {
        _playerSpawner.Spawned -= Configure;
    }

    private void InitializeDependencies()
    {
        Wallet.Instance.LoadMoney(DataProvider.Instance.GetMoney());
        _imageHandler.SetImage(DataProvider.Instance.GetImage());
        _playerSelectedCharacter.SetInitialCharacter(DataProvider.Instance.GetCharacter());
        _audioVolumeHandler.SetVolume(DataProvider.Instance.GetVolume());
        _levelText.SetSceneIndex(DataProvider.Instance.GetLevel());
        _sceneLoaderHandler.SetLevel(DataProvider.Instance.GetLevel());
        _enemyDestroyingHandler.SetCount(DataProvider.Instance.GetEnemyCount());
        _playerSpawner.SetCharacterId(DataProvider.Instance.GetCharacter());
    }

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