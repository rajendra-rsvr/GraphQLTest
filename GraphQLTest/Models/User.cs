﻿namespace GraphQLTest.Models
{
    public class User
    {
        [ID]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
