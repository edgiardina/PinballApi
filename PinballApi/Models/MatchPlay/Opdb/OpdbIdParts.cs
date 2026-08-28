using System;
using System.Text.RegularExpressions;

namespace PinballApi.Models.MatchPlay.Opdb
{
    /// <summary>
    /// An OPDB id split into its group, machine and alias parts.
    /// </summary>
    /// <remarks>
    /// An OPDB id has up to three parts. <c>G4ODR</c> is a machine group, <c>G4ODR-MLzY7</c> is a
    /// machine in that group, and <c>G0l8P-M85d9-A1ZNY</c> is an alias of a machine.
    /// </remarks>
    public class OpdbIdParts
    {
        private static readonly Regex Pattern = new Regex(
            @"^G([a-zA-Z0-9]+)(?:-M([a-zA-Z0-9]+)(?:-A([a-zA-Z0-9]+))?)?$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private OpdbIdParts(string group, string machine, string alias)
        {
            Group = group;
            Machine = machine;
            Alias = alias;
        }

        /// <summary>
        /// The group identifier, without the leading <c>G</c>.
        /// </summary>
        public string Group { get; }

        /// <summary>
        /// The machine identifier, without the leading <c>M</c>. Null on a machine group id.
        /// </summary>
        public string Machine { get; }

        /// <summary>
        /// The alias identifier, without the leading <c>A</c>. Null unless the id is an alias.
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// The kind of entry this id points at.
        /// </summary>
        public OpdbEntryType EntryType
        {
            get
            {
                if (Alias != null)
                {
                    return OpdbEntryType.Alias;
                }

                return Machine != null ? OpdbEntryType.Machine : OpdbEntryType.MachineGroup;
            }
        }

        /// <summary>
        /// The full OPDB id of the machine group this id belongs to.
        /// </summary>
        public string GroupId => "G" + Group;

        /// <summary>
        /// The full OPDB id of the machine this id belongs to. Null on a machine group id.
        /// </summary>
        public string MachineId => Machine == null ? null : GroupId + "-M" + Machine;

        /// <summary>
        /// The full OPDB id of the alias. Null unless the id is an alias.
        /// </summary>
        public string AliasId => Alias == null ? null : MachineId + "-A" + Alias;

        /// <summary>
        /// Splits an OPDB id into its parts.
        /// </summary>
        /// <param name="opdbId">The id to split, for example <c>G4ODR-MLzY7</c>.</param>
        /// <returns>The parts of the id.</returns>
        /// <exception cref="ArgumentException">The value is not a valid OPDB id.</exception>
        public static OpdbIdParts Parse(string opdbId)
        {
            if (TryParse(opdbId, out var parts))
            {
                return parts;
            }

            throw new ArgumentException($"'{opdbId}' is not a valid OPDB id.", nameof(opdbId));
        }

        /// <summary>
        /// Splits an OPDB id into its parts and reports whether the value is a valid id.
        /// </summary>
        /// <param name="opdbId">The id to split, for example <c>G4ODR-MLzY7</c>.</param>
        /// <param name="parts">The parts of the id, or null when the value is not valid.</param>
        /// <returns>True when the value is a valid OPDB id.</returns>
        public static bool TryParse(string opdbId, out OpdbIdParts parts)
        {
            parts = null;

            if (string.IsNullOrWhiteSpace(opdbId))
            {
                return false;
            }

            var match = Pattern.Match(opdbId);

            if (!match.Success)
            {
                return false;
            }

            parts = new OpdbIdParts(
                match.Groups[1].Value,
                match.Groups[2].Success ? match.Groups[2].Value : null,
                match.Groups[3].Success ? match.Groups[3].Value : null);

            return true;
        }

        /// <summary>
        /// Rebuilds the full OPDB id.
        /// </summary>
        public override string ToString() => AliasId ?? MachineId ?? GroupId;
    }
}
