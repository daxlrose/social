namespace Social.Application.Enums
{
    public enum ErrorCode
    {
        NotFound = 404,
        ServerError = 500,
        ValidationError = 101,
        IdentityUserAlreadyExists = 201,
        IdentityCreationFailed = 202,
        UnknownError = 999
    }
}
