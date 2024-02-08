using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using chatGPT5.models;

namespace chatGPT5.Models.network
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Content { get; set; } 
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        
        [ForeignKey("ChatRoom")]
        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
        public DateTime Timestamp { get; set; }
    }

}