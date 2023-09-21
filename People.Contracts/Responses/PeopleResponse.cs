namespace People.Contracts.Responses
{
    public class PeopleResponse
    {
        public required IEnumerable<PersonResponse> _People {  get; init; } = Enumerable.Empty<PersonResponse>();
    }
}
