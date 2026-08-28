namespace PinballApi.Models.MatchPlay.Opdb
{
    /// <summary>
    /// The kind of record an <see cref="OpdbEntry"/> represents.
    /// </summary>
    public enum OpdbEntryType
    {
        /// <summary>
        /// The entry type was missing or is not known to this library version.
        /// </summary>
        Unknown,

        /// <summary>
        /// A parent group that collects every edition of a title (OPDB id shaped <c>G#####</c>).
        /// </summary>
        MachineGroup,

        /// <summary>
        /// A physical machine (OPDB id shaped <c>G#####-M#####</c>).
        /// </summary>
        Machine,

        /// <summary>
        /// An alternate name for a machine (OPDB id shaped <c>G#####-M#####-A#####</c>).
        /// </summary>
        Alias
    }
}
