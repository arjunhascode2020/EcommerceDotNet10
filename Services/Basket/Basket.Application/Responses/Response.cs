namespace Basket.Application.Responses
{
    public record class Response
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}
