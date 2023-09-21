﻿namespace People.Contracts.Responses
{
    public class PersonResponse
    {

        //Those properties are init, meaning they can not be change in run time
        //It's not neccessary to used required if you have init,
        //but is the better pratice to include them
        public required Guid Id { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required int Gender { get; init; }
    }
}
