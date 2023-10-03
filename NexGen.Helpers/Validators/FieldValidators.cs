using System;
using System.Text.RegularExpressions;

namespace NexGen.Helpers.Validators;

public static class FieldValidators
{
    public static bool ValidateEmail(string email)
    {
        if (email is null) throw new ArgumentNullException(nameof(email), "Email cannot be null");

        var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        return emailRegex.IsMatch(email);
    }

    public static bool ValidatePassword(string password)
    {
        if (password is null) throw new ArgumentNullException(nameof(password), "Password cannot be null");

        var passwordRegex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");

        return passwordRegex.IsMatch(password);
    }
}