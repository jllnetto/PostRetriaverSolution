namespace PostsRetriver.Model;

public class Post
{
    public long Id { get; set; }
    public string Author { get; set; }
    public long AuthorId { get; set; }
    public long Likes { get; set; }
    public double Popularity { get; set; }
    public long Reads { get; set; }
    public List<string> Tags { get; set; }

}