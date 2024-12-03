using ChangeDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChangeDB.Contexts.ETC;

public class CompArticleETC : IEntityTypeConfiguration<CompArticle>
{
    public void Configure(EntityTypeBuilder<CompArticle> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("Comp.Article");
        entityTypeBuilder.HasKey(p => p.ArticleID);

    }
}
