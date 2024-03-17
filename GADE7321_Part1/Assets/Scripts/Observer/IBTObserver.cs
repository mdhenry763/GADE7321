public interface IBTObserver
{
    void OnNotify(string nodeName);
}

public interface IEntityObservers
{
    void OnFlagChange();
}