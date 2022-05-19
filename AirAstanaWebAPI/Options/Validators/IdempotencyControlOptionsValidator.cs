using Microsoft.Extensions.Options;

namespace AirAstanaWebAPI.Options.Validators
{
    public class IdempotencyControlOptionsValidator : IValidateOptions<IdempotencyControlOptions>
    {
		public ValidateOptionsResult Validate(string name, IdempotencyControlOptions options)
		{
			var failures = new List<string>();

			if (!options.IdempotencyFilterEnabled.HasValue)
			{
				failures.Add($"{nameof(options.IdempotencyFilterEnabled)} option is not found.");
			}

			if (string.IsNullOrWhiteSpace(options.ClientRequestIdHeader))
			{
				failures.Add($"{nameof(options.ClientRequestIdHeader)} option is not found.");
			}

			if (string.IsNullOrWhiteSpace(options.ClientRequestCacheKeyName))
			{
				failures.Add($"{nameof(options.ClientRequestCacheKeyName)} option is not found.");
			}

			if (options.ClientRequestCacheExpirationTimeout < TimeSpan.FromSeconds(15))
			{
				failures.Add($"{nameof(options.ClientRequestCacheExpirationTimeout)} value is lower than 15 seconds.");
			}

			if (failures.Count > 0)
			{
				return ValidateOptionsResult.Fail(failures);
			}

			return ValidateOptionsResult.Success;
		}
	}
}
