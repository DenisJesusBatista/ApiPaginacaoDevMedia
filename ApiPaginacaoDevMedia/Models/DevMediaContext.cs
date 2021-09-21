using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiPaginacaoDevMedia.Models
{
    public class DevMediaContext : DbContext
    {
        public DevMediaContext() : base("DevMediaLocalDb")
        {
            //Database.CreateIfNotExists();
            //Database.Log = d => System.Diagnostics.Debug.WriteLine(d);
            //< !--connectionString = "Server=DESKTOP-1OGBH22\SQLEXPRESS;Database=DevMediaLocalDb;User ID=sa;Password=123456;" /> -->

        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Aula> Aulas { get; set; }
    }
}