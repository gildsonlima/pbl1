using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pbl1.Models;

namespace pbl1.Models
{
    public class Pbl1Context : DbContext
    {
        public Pbl1Context (DbContextOptions<Pbl1Context> options)
            : base(options)
        {
        }

        public DbSet<Employee>? Employee { get; set; }
        public DbSet<Furniture>? Furniture { get; set; }
        public DbSet<User>? User { get; set; }
    }

}
