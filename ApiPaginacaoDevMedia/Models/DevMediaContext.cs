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
            Database.Log = d => System.Diagnostics.Debug.WriteLine(d);

        }

        public DbSet<Curso> Cursos { get; set; }
    }
}