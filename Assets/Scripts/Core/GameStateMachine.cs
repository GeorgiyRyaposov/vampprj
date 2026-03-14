/// <summary>
/// Глобальная стейт-машина игры. Хранит текущее состояние и переключает между ними.
/// Вызов EnterState(state) вызывает Exit() у текущего, затем Enter() у нового и сохраняет новое как текущее.
///
/// Как добавить новое состояние:
/// 1. Создайте класс в папке States, реализующий IState (Enter, Exit, Update).
/// 2. В нужном месте (другое состояние или сервис) получите машину: ServiceLocator.Instance.Get&lt;GameStateMachine&gt;().
/// 3. Вызовите machine.EnterState(new MyNewState(...)) для перехода. Регистрация по ключу не обязательна — можно создавать экземпляры состояний на лету.
/// </summary>
public class GameStateMachine
{
    private IState _currentState;

    /// <summary>
    /// Текущее активное состояние. Может быть null до первого перехода.
    /// </summary>
    public IState CurrentState => _currentState;

    /// <summary>
    /// Переключает машину в новое состояние: Exit() у текущего, Enter() у нового.
    /// </summary>
    /// <param name="state">Новое состояние; не должно быть null.</param>
    public void EnterState(IState state)
    {
        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    /// <summary>
    /// Обновляет текущее состояние каждый кадр. Вызывать из GameRunner.Update().
    /// </summary>
    public void Update()
    {
        _currentState?.Update();
    }
}
