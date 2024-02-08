using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using chatGPT5.Enums;
using chatGPT5.models;
using chatGPT5.Models.network;

public class User
{
    public User()
    {
        ChatRooms = new List<ChatRoom>();
        Messages = new List<Message>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public Roles Role { get; set; }

    public List<ChatRoom> ChatRooms { get; set; }

    public List<Message> Messages { get; set; }
}
