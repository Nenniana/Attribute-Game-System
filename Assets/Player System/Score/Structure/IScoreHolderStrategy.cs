using UnityEngine.Events;

public interface IScoreHolderStrategy
{
    UnityEvent<bool> HasWon {get;}
    UnityEvent<bool> HasLost {get;}
    void AddPositive(float score);
    void AddNegative(float score);
}