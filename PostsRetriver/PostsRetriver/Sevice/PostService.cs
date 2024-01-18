using PostsRetriver.Communication;
using PostsRetriver.Model;

namespace PostsRetriver.Sevice;

public class PostService : IPostService
{
    private IPostCommunicator _postCommunicator;

    public PostService(IPostCommunicator postCommunicator)
    {
        _postCommunicator = postCommunicator;
    }

    public async Task<List<Post>> GetPosts(List<string> tags, string sortBy = "Id", string direction = "asc")
    {
        List<Post> posts = new List<Post>();
        if (ValidadePatamether(tags, sortBy, direction))
        {
            if (tags.Any(x => x.Contains(",")))
            {
                List<string> aux = new List<string>();
                tags.ForEach(t=>
                {
                    aux.AddRange(t.Split(','));
                });
                tags = aux;
            }
            Parallel.ForEach(tags, async tag => { posts.AddRange(await _postCommunicator.GetPost(tag)); });
            posts = posts.GroupBy(p => p.Id).Select(group => group.First()).ToList();
            posts = SortList(posts, sortBy, direction);
                
        }
        return posts;
    }

    public bool ValidadePatamether(List<string> tags, string sortBy, string direction)
    {
        if (tags is null || !tags.Any())
        {
            throw new ApplicationException("Tags are invalid");
        }

        if (string.IsNullOrEmpty(sortBy) ||
            (sortBy != "id" && sortBy != "reads" && sortBy != "likes" && sortBy != "popularity"))
        {
            throw new ApplicationException("SortBy is Invalid");
        }

        if (string.IsNullOrEmpty(direction) || (direction != "desc" && direction != "asc"))
        {
            throw new ApplicationException("Direction is invalid");
        }

        return true;
    }

    private List<Post> SortList(List<Post> posts, string sortBy, string direction)
    {
        IOrderedEnumerable<Post> Orderedposts;
        if (direction.Trim() == "desc")
        {
            switch (sortBy.Trim())
            {
                case "reads":
                    Orderedposts = posts.OrderByDescending(x => x.Reads);
                    break;
                case "likes":
                    Orderedposts = posts.OrderByDescending(x => x.Likes);
                    break;
                case "popularity":
                    Orderedposts = posts.OrderByDescending(x => x.Popularity);
                    break;
                default:
                    Orderedposts = posts.OrderByDescending(x => x.Id);
                    break;
            }

            return Orderedposts.ToList();
        }

        switch (sortBy.Trim())
        {
            case "reads":
                Orderedposts = posts.OrderBy(x => x.Reads);
                break;
            case "likes":
                Orderedposts = posts.OrderBy(x => x.Likes);
                break;
            case "popularity":
                Orderedposts = posts.OrderBy(x => x.Popularity);
                break;
            default:
                Orderedposts = posts.OrderBy(x => x.Id);
                break;
        }

        return Orderedposts.ToList();
    }
}