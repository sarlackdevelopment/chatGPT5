﻿public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
}