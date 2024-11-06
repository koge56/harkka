using harkka.models;
using System.ComponentModel.DataAnnotations;

namespace harkka.models
{
    public class Message
    {
        public long id { get; set; }
        [MaxLength(100)]
        public string title { get; set; }
        [MaxLength(1000)]
        public string? body { get; set; }
        public User sender { get; set; }
        public User? recipent { get; set; }
        public Message? prevmessage { get; set; }
    }
    public class MessageDTO
    {
        public long id { get; set; }
        [MaxLength(100)]
        public string title { get; set; }
        [MaxLength(1000)]
        public string? body { get; set; }
        public string sender { get; set; }
        public string? recipent { get; set; }
        public long? PrevmessageId { get; set; }
    }

}