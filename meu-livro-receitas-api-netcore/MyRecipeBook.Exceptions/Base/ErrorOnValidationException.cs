namespace MyRecipeBook.Exceptions.Base;

public class ErrorOnValidationException(IList<string> errors) : MyRecipeBookException
{
    public IList<string> Errors { get; set; } = errors;
}