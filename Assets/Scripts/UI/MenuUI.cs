using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Обработчик кнопок главного меню. Вешается на GameObject в сцене Menu.
/// Кнопки ищутся по имени: StartButton, SettingsButton, ContinueButton, ExitButton.
/// </summary>
public class MenuUI : MonoBehaviour
{
    [Header("Имена кнопок в сцене (если пусто — используются значения по умолчанию)")]
    [SerializeField] private string startButtonName = "StartButton";
    [SerializeField] private string settingsButtonName = "SettingsButton";
    [SerializeField] private string continueButtonName = "ContinueButton";
    [SerializeField] private string exitButtonName = "ExitButton";

    [Header("Имя сцены уровня для «Начать игру»")]
    [SerializeField] private string levelSceneName = "Level";

    private Button _startButton;
    private Button _settingsButton;
    private Button _continueButton;
    private Button _exitButton;

    private void Start()
    {
        FindAndSubscribeButtons();
    }

    private void OnDestroy()
    {
        UnsubscribeButtons();
    }

    private void FindAndSubscribeButtons()
    {
        _startButton = FindButton(startButtonName);
        _settingsButton = FindButton(settingsButtonName);
        _continueButton = FindButton(continueButtonName);
        _exitButton = FindButton(exitButtonName);

        if (_startButton != null) _startButton.onClick.AddListener(OnStartGame);
        if (_settingsButton != null) _settingsButton.onClick.AddListener(OnSettings);
        if (_continueButton != null) _continueButton.onClick.AddListener(OnContinue);
        if (_exitButton != null) _exitButton.onClick.AddListener(OnExit);
    }

    private void UnsubscribeButtons()
    {
        if (_startButton != null) _startButton.onClick.RemoveListener(OnStartGame);
        if (_settingsButton != null) _settingsButton.onClick.RemoveListener(OnSettings);
        if (_continueButton != null) _continueButton.onClick.RemoveListener(OnContinue);
        if (_exitButton != null) _exitButton.onClick.RemoveListener(OnExit);
    }

    private static Button FindButton(string objectName)
    {
        var go = GameObject.Find(objectName);
        if (go == null)
        {
            Debug.LogWarning($"[MenuUI] Button '{objectName}' not found in scene.");
            return null;
        }

        var button = go.GetComponent<Button>();
        if (button == null)
            Debug.LogWarning($"[MenuUI] GameObject '{objectName}' has no Button component.");
        return button;
    }

    /// <summary> Начать игру — переход в GameState и загрузка уровня. </summary>
    private void OnStartGame()
    {
        var machine = ServiceLocator.Instance.Get<GameStateMachine>();
        machine.EnterState(new GameState(machine, levelSceneName));
    }

    /// <summary> Настройки — заглушка (можно открыть панель настроек или сцену). </summary>
    private void OnSettings()
    {
        Debug.Log("[MenuUI] Settings pressed — add your settings UI or scene.");
    }

    /// <summary> Продолжить — заглушка (загрузка сохранения). </summary>
    private void OnContinue()
    {
        Debug.Log("[MenuUI] Continue pressed — add save/load logic.");
    }

    /// <summary> Выход из игры. </summary>
    private void OnExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
