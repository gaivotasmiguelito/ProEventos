using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.API.Domain;


namespace ProEventos.Persistence.Contextos
{
    //Para fazer a tabela de contexto de Eventos
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options){}
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestranteEventos { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){

            modelBuilder.Entity<PalestranteEvento>().HasKey(PE => new { PE.EventoId, PE.PalestranteId });

            //Apaguei o Evento, apago tambem a rede social
            modelBuilder.Entity<Evento>().HasMany(e=>e.RedesSociais).WithOne(rs =>rs.Evento).OnDelete(DeleteBehavior.Cascade);

            //Apaguei o Palestrante, apago tambem a rede social
            modelBuilder.Entity<Palestrante>().HasMany(e=>e.RedesSociais).WithOne(rs =>rs.Palestrante).OnDelete(DeleteBehavior.Cascade);

        } 

    }
}