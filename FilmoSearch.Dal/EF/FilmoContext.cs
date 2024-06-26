﻿using FilmoSearch.Dal.Entity;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearch.Dal.EF
{
    public  class FilmoContext : DbContext
    {
        public DbSet<ActorEntity> Actors { get; set; } = null!;
        public DbSet<FilmEntity> Films { get; set; } = null!;
        public DbSet<GenreEntity> Genres { get; set; } = null!;
        public DbSet<ReviewEntity> Reviews { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;

        public FilmoContext(DbContextOptions<FilmoContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReviewEntity>()
                .ToTable(t => t.HasCheckConstraint("ValidStars", "\"Stars\" > 0 AND \"Stars\" < 6"));

            modelBuilder.Entity<ActorEntity>()
                .HasMany(a => a.Films)
                .WithMany(f => f.Actors)
                .UsingEntity(j => j.ToTable("ActorFilms"));

            modelBuilder.Entity<FilmEntity>()
                .HasMany(f => f.Genres)
                .WithMany(g => g.Films)
                .UsingEntity(j => j.ToTable("FilmGenres"));
        }
    }
}
