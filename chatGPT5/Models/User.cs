using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using chatGPT5.models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public ICollection<UserChatRoom>? UserChatRooms { get; set; }
}