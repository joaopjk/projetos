using Domain.Enums;

namespace Application.Responses
{
    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message{ get; set; }
        public ErrorCodes ErrorCodes { get; set; }
    }
}
