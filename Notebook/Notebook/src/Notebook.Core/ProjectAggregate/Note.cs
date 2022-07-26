using Notebook.SharedKernel;

namespace Notebook.Core.ProjectAggregate;
public class Note : EntityBase
{
  public string Title { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public string Url { get; set; } = string.Empty;

  public override string ToString()
  {
    return $"{Id}: Status: {Title} - {Content} - {Url}";
  }
}
