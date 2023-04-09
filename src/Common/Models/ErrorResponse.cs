using System.Text.Json;

namespace CodeExcercise.Common.Models
{
    public class ErrorResponse
    {
        public ErrorResponse(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public string Message { get; }
        public int StatusCode { get; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
