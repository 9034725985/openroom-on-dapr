namespace OpenroomDapr.Shared.Model;

public class Tag
{
    public Tag(string id, string text)
    {
        Id = id;
        Text = text;
    }

    public string Id { get; private set; }
    public string Text { get; set; }
    public List<Post> Posts { get; } = new();
}
