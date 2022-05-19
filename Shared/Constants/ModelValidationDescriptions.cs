namespace DBData.Shared.Constants
{
	internal static class ModelValidationDescriptions
	{
		public const string FieldCanNotBeEmpty = @"Поле не может быть пустым.";
		public const string StringValueLengthMustBeInRange = @"Длина строки может находиться в диапазоне от {MinLength} до {MaxLength} символов.";
		public const string StringValueLengthMustBeFixed = @"Длина строки должен быть {MaxLength} символа(-ов).";
		public const string DigitsScalePrecisionDetails = @"Поле не должен содержать более {ExpectedPrecision} цифр, с учетом {ExpectedScale} знака(-ов) после запятой. Для значения '{PropertyValue}' найдено {Digits} цифр и {ActualScale} десятичных знака";
		public const string DigitsCanBeInRange = @"Значение может находиться в диапазоне от {From} до {To}.";
		public const string DigitsCanNotBeNegative = @"Значение не может быть отрицательным.";
		//public const string IncorrectIinBin = @"Неверный ИИН/БИН '{PropertyValue}'.";
		public const string IncorrectValue = @"Неверное значение '{PropertyValue}'.";
		public const string IncorrectIban = @"Некорректно указан IBAN-счет.";
		public const string MaxCollectionLength = "Коллекция может содержать до '{MaxLenght}' элементов.";

		public const string DateFormatRegex = @"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{4})$";
		public const string DateFormat = @"Неверный формат даты. Формат должен быть ДД.ММ.ГГГГ";
		public static readonly string[] DateFormats = { @"dd.MM.yyyy", @"dd/MM/yyyy", @"dd-MM-yyyy" };

		public const string BinIinValueRegex = @"^\d{12}$";
		public const string BinIinValueIncorrect = @"Неверный ИИН/БИН '{PropertyValue}'";

		/// <summary>
		/// https://ru.wikipedia.org/wiki/ISO_9362
		/// </summary>
		public const string BicValueRegex = @"^[A-Z]{4}[A-Z]{2}[A-Z0-9]{2}([A-Z0-9]{3})?$";
		public const string BicValueIncorrect = @"Некорректно указан БИК.";

		/// <summary>
		/// https://online.zakon.kz/Document/?doc_id=35263852
		/// </summary>
		public const string BeneficiaryCodeValueRegex = @"^[1-2]{1}[1-9]{1}$";
		public const string BeneficiaryCodeValueIncorrect = @"Некорректно указан Код бенефициара (КБе).";

		/// <summary>
		/// https://online.zakon.kz/Document/?doc_id=38798144
		/// </summary>
		public const string PaymentPurposeCodeValueRegex = @"^\d{3}$";
		public const string PaymentPurposeCodeValueIncorrect = @"Некорректно указан Код назначения платежа (КНП).";

		public const string StringValueIsExists = @"Значение '{PropertyValue}' уже существует.";
		public const string StringContainsOnlyEnglishLettersUpperCaseSensitiveRegex = @"^[A-Z]*$";
		public const string StringContainsOnlyEnglishLettersUpperCaseSensitive = @"Строка может содержать только буквы латинского алфавита (A-Z) в верхнем регистре.";

		public const string StringValueCanNotStartOrEndWithSpacesRegex = @"^\S+[\w\W]*\S+$";
		public const string StringValueCanNotStartOrEndWithSpaces = @"Строка не должна начинаться или заканчиваться пробелом.";

		public const string StringContainsLatinCyrillicLettersSpaceAndBracketsCaseInsensitiveRegex = @"^\S+[a-zA-Zа-яА-ЯёЁ\s()]*\S+$";
		public const string StringContainsLatinCyrillicLettersSpaceAndBracketsCaseInsensitive = @"Строка может содержать только буквы кириллического (аА-яЯ) и латинского алфавита (aA-zZ) без учета регистра, скобки и пробел.";

		public const string StringContainsLatinCyrillicLettersAndDigitsWithUnderscoreSpaceDashDotQuoteAndBracketsCaseInsensitiveRegex = @"^\S+[-_0-9a-zA-Zа-яА-ЯёЁ\s()\""\.]*\S+$";
		public const string StringContainsLatinCyrillicLettersAndDigitsWithUnderscoreSpaceDashDotQuoteAndBracketsCaseInsensitive = @"Строка может содержать только буквы кириллического (аА-яЯ) и латинского алфавита (aA-zZ) без учета регистра, цифры (0-9), скобки, пробелы, знак подчеркивания и специальные символы (точка, дефис, кавычка).";

		public const string StringContainsOnlyDigits = @"Строка может содержать только цифры (0-9).";

		public const string StringContainsOnlyEnglishLettersAndDigitsCaseInsensitiveRegex = @"^[0-9a-zA-Z]*$";
		public const string StringContainsOnlyEnglishLettersAndDigitsCaseInsensitive = @"Строка может содержать только буквы латинского алфавита (aA-zZ) и цифры (0-9) без учета регистра.";

		public const string StringContainsOnlyEnglishLettersAndDigitsWithUnderscoreCaseInsensitiveRegex = @"^[_0-9a-zA-Z]*$";
		public const string StringContainsOnlyEnglishLettersAndDigitsWithUnderscoreCaseInsensitive = @"Строка может содержать только буквы латинского алфавита (aA-zZ) без учета регистра, цифры (0-9) и знак подчеркивания (_) в качестве пробела.";
	}
}
