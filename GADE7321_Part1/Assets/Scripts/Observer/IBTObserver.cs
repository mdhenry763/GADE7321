public interface IBTObserver
{
    void OnNotify(string nodeName);

    void OnAIStateChange(AIState aiState);
}

public interface IEntityObservers
{
    void OnFlagChange();
}