﻿namespace FilmoSearch.Bll.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateOnly? RegistrationDate { get; set; }

        public IEnumerable<ReviewModel>? Reviews { get; set; }
    }
}
