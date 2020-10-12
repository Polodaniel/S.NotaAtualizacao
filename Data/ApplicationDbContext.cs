using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using S.NotaAtualizacao.Models;

namespace S.NotaAtualizacao.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<S.NotaAtualizacao.Models.NotaAtualizacaoItem> NotaAtualizacaoItem { get; set; }
        //public DbSet<S.NotaAtualizacao.Models.NotaAtualizacaoItem> NotaAtualizacao { get; set; }
    }
}
