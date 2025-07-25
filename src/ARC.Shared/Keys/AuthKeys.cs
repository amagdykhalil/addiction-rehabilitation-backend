﻿namespace ARC.Shared.Keys
{
    public static partial class LocalizationKeys
    {
        public static class Auth
        {
            public static string InvalidCredentials { get; private set; }
            public static string InvalidToken { get; private set; }
            public static string TokenTooLong { get; private set; }
            public static string EmailConfirmed { get; private set; }
            public static string EmailConfirmationSent { get; private set; }
            public static string PasswordResetSent { get; private set; }
            public static string PasswordResetSuccess { get; private set; }
            public static string InvalidResetCode { get; private set; }
            public static string ShouldConfirmEmail { get; private set; }
            public static string InactiveUser { get; private set; }
        }
    }
}