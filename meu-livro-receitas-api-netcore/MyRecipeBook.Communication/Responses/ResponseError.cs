namespace MyRecipeBook.Communication.Responses;

public class ResponseError(IList<string> errors)
{
    public IList<string> Errors { get; init; } = errors;
}