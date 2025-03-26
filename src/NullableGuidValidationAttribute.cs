using System.ComponentModel.DataAnnotations;
using Soenneker.Extensions.String;

namespace Soenneker.ValidationAttributes.NullableGuid;

/// <summary>
/// A validation attribute that ensures a nullable string is a valid, nullable GUID. If this value is not null, checks to make sure the GUID can be parsed. <para/>
/// If the value is null, this passes.
/// </summary>
public class NullableGuidValidationAttribute : ValidationAttribute
{
    public static string GetErrorMessage(string? fieldName)
    {
        if (fieldName == null)
            return "Field is not valid GUID";

        return $"Field \"{fieldName}\" is not valid GUID";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext? validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        if (value is not string s)
            return null;

        if (s.IsValidPopulatedNullableGuid())
            return ValidationResult.Success;

        return new ValidationResult(GetErrorMessage(validationContext?.MemberName));
    }
}