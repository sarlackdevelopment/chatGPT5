﻿public class Message
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Content { get; set; }
    public User Sender { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}