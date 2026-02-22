namespace Catalog.Application.Responses
{
    public record Response
    {
        public bool Success { get; init; }

        public string Message { get; set; }
    }
}
