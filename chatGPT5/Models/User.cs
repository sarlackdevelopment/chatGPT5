using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using chatGPT5.models;

public class User
{
    public User()
    {
        ChatRooms = new List<ChatRoom>();
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public List<ChatRoom> ChatRooms { get; set; }
}