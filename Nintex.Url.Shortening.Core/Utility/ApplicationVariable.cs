namespace Nintex.Url.Shortening.Core.Utility
{
    public static class ApplicationVariable
    {
        public static string JwtSigninKey = "dtqw!@#$8QA852djaYYDl5amHGFPadg qawdea ;adjgdyare)&45439*/aghd adgrtyudad; pkreYCVNSItacvyo%$ja@*";
        public static string RequiredFieldsMessage = "Required fields cannot be empty";
        public static string UserAlreadyExist = "User already exist";
        public static string ClaimAccountId = "AccountId";
        public static string PasswordDoesNotMatch = "Password is not match";
        public static int KeyLength => 8;
        public static int ShortUrlExpireDays => 30;
        public static string UrlPatten => @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)";
    }
}
