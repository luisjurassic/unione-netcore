namespace Unione.Net;

public static class UniConstants
{
    private enum ErrorCodes
    {
        OK = 200,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        RequestEntityTooLarge = 413,
        TooManyRequest = 429,
        InternalServerError = 500
    }
}