﻿namespace CarWorkshop.Application.AplicationUser
{
    public class CurrentUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<String> Roles { get; set; }

        public CurrentUser(string id, string email, IEnumerable<string> roles)
        {
            Id = id;
            Email = email;
            Roles = roles;
        }

        public bool IsInRole(string role) => Roles.Contains(role);
    }
}
