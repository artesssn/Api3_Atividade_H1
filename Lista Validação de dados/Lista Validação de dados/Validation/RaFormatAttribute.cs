namespace Lista_Validação_de_dados.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class RaFormatAttribute : ValidationAttribute
    {
        private static readonly Regex RaRegex = new(@"^RA\d{6}$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        public RaFormatAttribute()
        {
            ErrorMessage = "RA deve começar com 'RA' seguido de 6 dígitos. Ex: RA123456";
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }

            var stringValue = value.ToString()!.Trim().ToUpperInvariant();
            return RaRegex.IsMatch(stringValue);
        }
    }
}
