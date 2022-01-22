using Chat.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserMessage> UserMessages { get; set; }
        public ApplicationContext() : base("DefaultConnection") { }
    }
}
