namespace MyRecipeBook.Communication.Responses;

public class ResponseError
{
    public List<string> Errors { get; private set; }

    public ResponseError(List<string> errors) => Errors = errors;

    public ResponseError(string error) => Errors = [error];
}