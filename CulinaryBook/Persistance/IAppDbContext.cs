using CulinaryBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryBook.Persistance
{
    public interface IAppDbContext
    {
        DbSet<Recipe> Recipes { get; set; }

        Task<int> SaveChangesAsync();

    }
}
