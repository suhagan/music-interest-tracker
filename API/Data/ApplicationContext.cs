using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class ApplicationContext : DbContext
  {
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; set; }
    public DbSet<Artist> Artist { get; set; }
    public DbSet<Genre> Genre { get; set; }
    public DbSet<Song> Song { get; set; }
  }
}