using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CCDC.Models
{
    public class PalContext : DbContext
    {
        public PalContext(DbContextOptions<PalContext> options)
            : base(options)
        {
        }

        public DbSet<PalItem> Palindromes { get; set; } = null!;
    }
}
