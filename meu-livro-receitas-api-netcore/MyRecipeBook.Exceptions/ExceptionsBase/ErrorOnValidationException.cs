namespace MyRecipeBook.Exceptions.ExceptionsBase;

public class ErrorOnValidationException(List<string> errors) : MyRecipeBookException
{
    private readonly List<string> _errors = errors;
    
    public List<string> GetErrors() => _errors;
}