# Auth Endpoints Migration

This document describes the new authentication endpoints that have been created to replace the `CustomIdentityApiEndpointRouteBuilderExtensions` functionality.

## Overview

The following endpoints have been created following the same pattern as the existing login use case:

1. **ConfirmEmail** - GET endpoint for email confirmation
2. **ResendConfirmationEmail** - POST endpoint to resend confirmation email
3. **ForgotPassword** - POST endpoint to send password reset email
4. **ResetPassword** - POST endpoint to reset password

## New Commands and Handlers

### 1. ConfirmEmail

**Command**: `ConfirmEmailCommand`

- **Location**: `src/Core/ARC.Application/Features/Auth/Commands/ConfirmEmail/`
- **Parameters**: `UserId`, `Code`, `ChangedEmail` (optional)
- **Handler**: `ConfirmEmailCommandHandler`
- **Endpoint**: `GET /api/auth/confirm-email`

### 2. ResendConfirmationEmail

**Command**: `ResendConfirmationEmailCommand`

- **Location**: `src/Core/ARC.Application/Features/Auth/Commands/ResendConfirmationEmail/`
- **Parameters**: `Email`
- **Handler**: `ResendConfirmationEmailCommandHandler`
- **Validator**: `ResendConfirmationEmailCommandValidator`
- **Endpoint**: `POST /api/auth/resend-confirmation-email`

### 3. ForgotPassword

**Command**: `ForgotPasswordCommand`

- **Location**: `src/Core/ARC.Application/Features/Auth/Commands/ForgotPassword/`
- **Parameters**: `Email`
- **Handler**: `ForgotPasswordCommandHandler`
- **Validator**: `ForgotPasswordCommandValidator`
- **Endpoint**: `POST /api/auth/forgot-password`

### 4. ResetPassword

**Command**: `ResetPasswordCommand`

- **Location**: `src/Core/ARC.Application/Features/Auth/Commands/ResetPassword/`
- **Parameters**: `Email`, `ResetCode`, `NewPassword`
- **Handler**: `ResetPasswordCommandHandler`
- **Validator**: `ResetPasswordCommandValidator`
- **Endpoint**: `POST /api/auth/reset-password`

## Updated Services

### IIdentityService Interface

Added the following methods to support email confirmation and password reset:

```csharp
// Email confirmation methods
Task<User?> FindByIdAsync(string userId);
Task<User?> FindByEmailAsync(string email);
Task<bool> IsEmailConfirmedAsync(User user);
Task<IdentityResult> ConfirmEmailAsync(User user, string code);
Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string code);
Task<IdentityResult> SetUserNameAsync(User user, string userName);
Task<string> GenerateEmailConfirmationTokenAsync(User user);
Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail);

// Password reset methods
Task<string> GeneratePasswordResetTokenAsync(User user);
Task<IdentityResult> ResetPasswordAsync(User user, string code, string newPassword);
```

### IdentityService Implementation

All new methods have been implemented in the `IdentityService` class to delegate to the underlying `UserManager<T>`.

## Updated AuthController

The `AuthController` has been extended with the following new endpoints:

1. `GET /api/auth/confirm-email` - Email confirmation
2. `POST /api/auth/resend-confirmation-email` - Resend confirmation email
3. `POST /api/auth/forgot-password` - Send password reset email
4. `POST /api/auth/reset-password` - Reset password

## Localization

### New Auth Keys

Added the following localization keys to `LocalizationKeys.Auth`:

- `EmailConfirmed`
- `EmailConfirmationSent`
- `PasswordResetSent`
- `PasswordResetSuccess`

### New Validation Keys

Added the following validation key to `LocalizationKeys.Validation`:

- `ResetCodeRequired`

### Localization Files

Updated both English (`en/auth.json`, `en/validation.json`) and Arabic (`ar/auth.json`, `ar/validation.json`) localization files with the new messages.

## Testing

Created unit tests for the `ConfirmEmailCommandHandler` to verify:

- Valid confirmation code returns success
- User not found returns error
- Invalid code format returns error
- Confirmation failure returns error

## Usage Examples

### Email Confirmation

```http
GET /api/auth/confirm-email?userId=123&code=abc123
```

### Resend Confirmation Email

```http
POST /api/auth/resend-confirmation-email
Content-Type: application/json

{
  "email": "user@example.com"
}
```

### Forgot Password

```http
POST /api/auth/forgot-password
Content-Type: application/json

{
  "email": "user@example.com"
}
```

### Reset Password

```http
POST /api/auth/reset-password
Content-Type: application/json

{
  "email": "user@example.com",
  "resetCode": "abc123",
  "newPassword": "NewPassword123!"
}
```

## Security Considerations

1. **Email Confirmation**: Uses base64url-encoded tokens for security
2. **Password Reset**: Validates email confirmation status before allowing reset
3. **Error Handling**: Does not reveal whether a user exists or not for security
4. **Token Validation**: Properly validates and decodes tokens before use

## Next Steps

1. Delete the `CustomIdentityApiEndpointRouteBuilderExtensions.cs` file
2. Remove any references to the old endpoints in your application
3. Update any frontend applications to use the new endpoint URLs
4. Consider adding integration tests for the new endpoints
5. Update API documentation to reflect the new endpoints

## Dependencies

The new endpoints use the following existing services:

- `IIdentityService` (extended)
- `IEmailSender<User>`
- `IStringLocalizer<T>`
- `PasswordValidator<T>`

All dependencies are automatically registered through the existing dependency injection setup.
