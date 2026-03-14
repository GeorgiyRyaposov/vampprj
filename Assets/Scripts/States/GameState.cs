using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Состояние геймплея: загрузка уровня и логика игры.
/// Добавить новое состояние по аналогии: класс с IState, в Enter — LoadSceneAsync уровня,
/// в Update — геймплей, в Exit — выгрузка/сохранение и при необходимости переход в меню.
/// </summary>
public class GameState : IState
{
    private readonly GameStateMachine _machine;
    private readonly string _levelSceneName;
    private AsyncOperation _loadOp;
    private bool _levelReady;

    public GameState(GameStateMachine machine, string levelSceneName)
    {
        _machine = machine;
        _levelSceneName = levelSceneName;
    }

    public void Enter()
    {
        _levelReady = false;
        _loadOp = SceneManager.LoadSceneAsync(_levelSceneName);
        if (_loadOp == null)
            Debug.LogWarning($"[GameState] Scene '{_levelSceneName}' not found. Add it to Build Settings.");
    }

    public void Exit()
    {
        _loadOp = null;
        _levelReady = false;
        // При необходимости: сохранение прогресса, выгрузка уровня.
    }

    public void Update()
    {
        if (_loadOp != null && !_loadOp.isDone)
            return;
        _levelReady = true;
        // Здесь — обновление геймплея (или делегировать системам/сущностям на сцене).
    }
}
