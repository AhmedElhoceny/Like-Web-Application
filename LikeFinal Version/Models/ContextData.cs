using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikeFinal_Version.Models
{
    public class ContextData : DbContext
    {
        public ContextData(DbContextOptions<ContextData> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Youtubes> Youtubes { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Instegram> Instegram { get; set; }
        public DbSet<InstegramOperations> InstegramOperations { get; set; }
        public DbSet<Facebook> Facebook { get; set; }
        public DbSet<FacebookOperations> FacebookOperations { get; set; }
        public DbSet<Twitter> Twitter { get; set; }
        public DbSet<TwitterOperations> TwitterOperations { get; set; }
        public DbSet<TikTok> TikTok { get; set; }
        public DbSet<TikTokOperations> TikTokOperations { get; set; }
        public DbSet<ShareLinkOperations> ShareLinkOperations { get; set; }



    }
}
