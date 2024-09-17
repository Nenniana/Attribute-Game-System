using UnityEngine.Events;

public class WinLossScoreHolderStrategy : IScoreHolderStrategy
{
    private int wins = 0;
    private int losses = 0;
    private UnityEvent<bool> hasWon = new UnityEvent<bool>();
    private UnityEvent<bool> hasLost = new UnityEvent<bool>();

    public UnityEvent<bool> HasWon => hasWon;
    public UnityEvent<bool> HasLost => hasLost;

    public void AddNegative(float score)
    {
        losses += (int) score;

        CheckIfLost();
    }

    public void AddPositive(float score)
    {
        wins += (int) score;

        CheckIfWon();
    }

    private void CheckIfWon() {
        if (wins >= 9)
            HasWon?.Invoke(true);
    }

    private void CheckIfLost() {
        if (losses >= 3)
            HasLost?.Invoke(true);
    }
}