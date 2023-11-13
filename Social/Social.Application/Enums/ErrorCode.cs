namespace Social.Application.Enums
{
    public enum ErrorCode
    {
        NotFound = 404,
        ServerError = 500,
        ValidationError = 101,
        IdentityUserAlreadyExists = 201,
        IdentityCreationFailed = 202,
        IdentityUserDoesNotExist = 203,
        IncorrectPassword = 204,
        PostUpdateNotPossible = 300,
        PostDeleteNotPossible = 301,
        UnknownError = 999
    }
}
