namespace IntegrifyLibrary.Business;

public class CustomException : Exception
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }

    public CustomException(int statusCode = 500, string message = "Internal server error") : base(message)
    {
        StatusCode = statusCode;
        ErrorMessage = message;
    }

    public CustomException NotFoundException(string message = "Item cannot be found")
    {
        return new CustomException(404, message);
    }

    public CustomException BadRequestException(string message = "Bad request")
    {
        return new CustomException(400, message);
    }

    public CustomException UnauthorizedException(string message = "Unauthorized")
    {
        return new CustomException(401, message);
    }

}
