using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Состояние инициализации. Регистрирует сервисы в ServiceLocator и переходит в меню.
/// Добавить новое состояние по аналогии: создать класс, реализующий IState, в Enter/Exit/Update
/// выполнить нужные действия и при необходимости вызвать _machine.EnterState(new NextState(...)).
/// </summary>
public class BootstrapState : IState
{
    private readonly GameStateMachine _machine;
    private const string MenuSceneName = "Menu";

    public BootstrapState(GameStateMachine machine)
    {
        _machine = machine;
    }

    public void Enter()
    {
        // Регистрация дополнительных сервисов (машина и локатор уже созданы в GameRunner).
        // Пример: ServiceLocator.Instance.Register(new AudioService());
        // Загрузка начальных данных при необходимости.

        // Переход в меню. Имя сцены меню можно вынести в конфиг или константу.
        _machine.EnterState(new MenuState(_machine, MenuSceneName));
    }

    public void Exit()
    {
        // Инициализация одноразовая, очистка при выходе обычно не требуется.
    }

    public void Update()
    {
        // Bootstrap сразу переключается в MenuState в Enter(), Update не используется.
    }
}
