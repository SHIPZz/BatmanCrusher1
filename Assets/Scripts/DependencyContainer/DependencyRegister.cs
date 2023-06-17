using UnityEngine;

public class DependencyRegister : MonoBehaviour
{
    private GameFactory _gameFactory = new();
    private GoldMoneyPaySystem _goldMoneyPaySystem = new GoldMoneyPaySystem();

    private void Awake()
    {
        DependencyContainer.Register<GameFactory>(_gameFactory);
        DependencyContainer.Register<GoldMoneyPaySystem>(_goldMoneyPaySystem);
    }
}