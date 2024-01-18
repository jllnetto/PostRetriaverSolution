using PostsRetriver.Model;

namespace PostsRetriver.Communication;

public interface IPostCommunicator
{
    public Task<List<Post>> GetPost(string tag);
}