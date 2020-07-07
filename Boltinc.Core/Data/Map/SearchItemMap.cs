
using System;
using Boltinc.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Core.Data.Map
{
    public class SearchItemMap : IEntityTypeConfiguration<SearchItem>
    {
        public void Configure(EntityTypeBuilder<SearchItem> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(x => x.SearchEngine)
                .IsRequired()
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .HasConversion(
                    v => v.ToString("G"),
                    v => (SearchEngineType)Enum.Parse(typeof(SearchEngineType), v)); ;

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(1000);

            builder.ToTable("SearchItem");
        }
    }
}
