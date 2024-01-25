using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{

  public class Genre
  {
    public int Id { get; set; }
    public string Title { get; set; }

    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<Song> Songs { get; set; }
  }
}