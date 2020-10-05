namespace Skoggy.Grove.StateMachines
{
    public interface IState
    {
        void OnEnter();
        void OnLeave();
        void OnUpdate();
    }
}