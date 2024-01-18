using PostsRetriver.Model;

namespace PostsRetriver.Sevice;

public interface IPostService
{
    public Task<List<Post>> GetPosts(List<string> tags,string sortBy,string direction);
    public bool ValidadePatamether(List<string> tags, string sortBy, string direction);
}