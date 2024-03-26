public interface IBTObserver //Interface for observer
{
    void OnNotify(string nodeName);

    void OnAIStateChange(AIState aiState); 
}

public interface IEntityObservers
{
    void OnFlagChange();
}