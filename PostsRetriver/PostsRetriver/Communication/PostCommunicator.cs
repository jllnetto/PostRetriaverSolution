using Newtonsoft.Json;
using PostsRetriver.Model;
using PostsRetriver.Model.DTO;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PostsRetriver.Communication;

public class PostCommunicator : IPostCommunicator
{
    private HttpClient _client;

    public PostCommunicator()
    {
        _client = new HttpClient();
    }

    public async Task<List<Post>> GetPost(string tag)
    {
        
            var result =  _client.GetAsync(@"https://api.hatchways.io/assessment/blog/posts?tag=" + tag.Trim()).Result;
            result.EnsureSuccessStatusCode();
            string responseBody = await result.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<PostsContainer>(responseBody);
            
            return posts.Posts;
        
    }
}