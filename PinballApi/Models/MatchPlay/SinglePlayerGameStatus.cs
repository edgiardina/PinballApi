namespace PinballApi.Models.MatchPlay
{
    /// <summary>
    /// The state of a single player game. Single player games add <see cref="Pending"/> to the
    /// states a multi-player <see cref="GameStatus"/> can have.
    /// </summary>
    public enum SinglePlayerGameStatus
    {
        Pending,
        Started,
        Completed
    }
}
