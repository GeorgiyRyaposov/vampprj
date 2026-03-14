using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Состояние меню: загрузка сцены меню и логика меню.
/// Добавить новое состояние по аналогии: новый класс с IState, в конструктор передать GameStateMachine
/// и при необходимости параметры (имя сцены и т.д.), в Enter — загрузка/подписки, в Exit — отписка.
/// </summary>
public class MenuState : IState
{
    private readonly GameStateMachine _machine;
    private readonly string _menuSceneName;
    private AsyncOperation _loadOp;

    public MenuState(GameStateMachine machine, string menuSceneName)
    {
        _machine = machine;
        _menuSceneName = menuSceneName;
    }

    public void Enter()
    {
        _loadOp = SceneManager.LoadSceneAsync(_menuSceneName);
        if (_loadOp == null)
            Debug.LogWarning($"[MenuState] Scene '{_menuSceneName}' not found. Add it to Build Settings.");
        // Подписки на кнопки меню (Play, Settings и т.д.) лучше вешать на UI в сцене;
        // для перехода в игру: ServiceLocator.Instance.Get&lt;GameStateMachine&gt;().EnterState(new GameState(..., "Level1"));
    }

    public void Exit()
    {
        _loadOp = null;
        // Отписка от UI, при необходимости выгрузка сцены.
    }

    public void Update()
    {
        // Ожидание завершения загрузки сцены или обновление логики меню.
        if (_loadOp != null && !_loadOp.isDone)
            return;
    }
}
