using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HelloWorld.Models
{
    public class MessageContext : DbContext
    {
        public MessageContext (DbContextOptions<MessageContext> options)
            : base(options)
        {
        }

        public DbSet<HelloWorld.Models.Message> Message { get; set; }
    }
}
