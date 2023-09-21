namespace People.Contracts.Responses
{
    public class ValidationFailureResponse
    {
        public required IEnumerable<ValidationResponse> Errors { get; set; }
    }

    public class ValidationResponse
    {
        public required string ProtertyName { get; init; }
        public required string Message { get; init; }
    }
}
