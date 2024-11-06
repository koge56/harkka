using System.Collections.Generic;
using harkka.models;
using Microsoft.EntityFrameworkCore;

namespace harkka.models
{
    public class MessageServiceContext : DbContext
    {
        public MessageServiceContext(DbContextOptions<MessageServiceContext> options)
        : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
