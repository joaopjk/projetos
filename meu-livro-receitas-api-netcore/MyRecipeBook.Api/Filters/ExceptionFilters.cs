using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.Base;

namespace MyRecipeBook.Api.Filters;

public abstract class ExceptionFilters : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is MyRecipeBookException)
            HandleException(context);
        else
            ThrowUnknowException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        if (context.Exception is not ErrorOnValidationException exception) return;
        
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new BadRequestObjectResult(new ResponseError(exception.Errors));
    }

    private void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult("Ocorreu um erro inesperado!");
    }
}