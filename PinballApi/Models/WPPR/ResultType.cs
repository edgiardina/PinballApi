namespace PinballApi.Models.WPPR
{
    public enum ResultType
    {
        /// <summary>
        /// Currently Used in the top 20 results and contributes towards the player's overall rank
        /// </summary>
        Active,
        /// <summary>
        /// Not currently used in the top 20 results because the WPPR value is too low compared to other active results
        /// </summary>
        NonActive,
        /// <summary>
        /// Outside of the 3 year window of results, so the WPPR points achieved no longer contribute to current rank
        /// </summary>
        Inactive
    }
}