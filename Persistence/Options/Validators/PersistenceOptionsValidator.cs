using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Persistence.Options.Validators
{
	public class PersistenceOptionsValidator : IValidateOptions<PersistenceOptions>
	{
		public ValidateOptionsResult Validate(string name, PersistenceOptions options)
		{
			var failures = new List<string>();

			if (string.IsNullOrWhiteSpace(options.ConnectionString))
			{
				failures.Add($"{nameof(options.ConnectionString)} option is not found.");
			}

			if (failures.Count > 0)
			{
				return ValidateOptionsResult.Fail(failures);
			}

			return ValidateOptionsResult.Success;
		}
	}
}
