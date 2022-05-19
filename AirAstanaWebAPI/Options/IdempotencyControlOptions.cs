namespace AirAstanaWebAPI.Options
{
    public class IdempotencyControlOptions
    {
        public bool? IdempotencyFilterEnabled { get; set; }
        public string ClientRequestIdHeader { get; set; }
        public string ClientRequestCacheKeyName { get; set; }
        public TimeSpan ClientRequestCacheExpirationTimeout { get; set; }
        public int ApiRequestFilterMilliseconds { get; set; } = 60_000;
    }
}
