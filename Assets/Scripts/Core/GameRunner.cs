using UnityEngine;

/// <summary>
/// Точка входа глобальной стейт-машины. Повесьте на GameObject в стартовой сцене (первой в Build Settings).
/// Инициализирует ServiceLocator и GameStateMachine, запускает с BootstrapState, каждый кадр обновляет текущее состояние.
/// </summary>
public class GameRunner : MonoBehaviour
{
    private GameStateMachine _stateMachine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var locator = ServiceLocator.Instance;
        _stateMachine = new GameStateMachine();
        locator.Register(_stateMachine);

        // Старт с инициализации; BootstrapState сам переключится в MenuState.
        _stateMachine.EnterState(new BootstrapState(_stateMachine));
    }

    private void Update()
    {
        _stateMachine.Update();
    }
}
