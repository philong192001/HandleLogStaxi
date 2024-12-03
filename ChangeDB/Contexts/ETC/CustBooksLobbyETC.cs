using ChangeDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChangeDB.Contexts.ETC;

public class CustBooksLobbyETC : IEntityTypeConfiguration<CustBooksLobby>
{
    public void Configure(EntityTypeBuilder<CustBooksLobby> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("Cust.BooksLobby");
        entityTypeBuilder.Property(p => p.BookId).HasColumnType("uniqueidentifier");
        entityTypeBuilder.Property(p => p.PublicBookId).HasColumnType("INT").ValueGeneratedOnAdd();
        entityTypeBuilder.HasKey(p => p.BookId);
    }
}
