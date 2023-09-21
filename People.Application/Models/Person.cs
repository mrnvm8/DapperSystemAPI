namespace People.Application.Models
{
    public class Person
    {
        public required Guid Id { get; init; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required Gender Gender { get; init; }
    }
}
