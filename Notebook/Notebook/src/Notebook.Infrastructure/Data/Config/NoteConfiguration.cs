using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Core.ProjectAggregate;

namespace Notebook.Infrastructure.Data.Config;
public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
  public void Configure(EntityTypeBuilder<Note> builder)
  {
    builder.Property(t => t.Title)
      .IsRequired();
  }
}
