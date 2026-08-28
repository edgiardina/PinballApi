namespace PinballApi.Models.MatchPlay.Opdb
{
    /// <summary>
    /// What happened to an OPDB id in the changelog.
    /// </summary>
    public enum OpdbChangelogAction
    {
        /// <summary>
        /// The action was missing or is not known to this library version.
        /// </summary>
        Unknown,

        /// <summary>
        /// The id was replaced by the id in <see cref="OpdbChangelogEntry.OpdbIdReplacement"/>.
        /// </summary>
        Move,

        /// <summary>
        /// The id was removed and has no replacement.
        /// </summary>
        Delete
    }
}
