using Newtonsoft.Json;

namespace PostsRetriver.Model.DTO;

public class PostsContainer
{
    [JsonProperty("posts")]
    public List<Post> Posts { get; set; }
}