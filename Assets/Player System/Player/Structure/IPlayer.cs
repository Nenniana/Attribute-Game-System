namespace Player {
    public interface IPlayer : IHealable, IDamagable, IKillable
    {
        string Name {get;}
        string Id {get;}
        IScoreHolderStrategy ScoreStrategy {get;}
        //TODO Board board {get;}
        //TODO Hand hand {get;}
        //TODO Deck deck {get;}
    }
}