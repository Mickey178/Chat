using Chat.Common;
using System.Data.Entity;

namespace Chat
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserMessage> UserMessages { get; set; }
        public ApplicationContext() : base("DefaultConnection") { }
    }
}
