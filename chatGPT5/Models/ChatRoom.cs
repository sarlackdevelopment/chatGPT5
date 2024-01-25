namespace chatGPT5.models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChatRoom
{
    public ChatRoom()
    {
        Users = new List<User>();
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    // public ICollection<UserChatRoom>? UserChatRooms { get; set; }
    public List<User> Users { get; set; }
}