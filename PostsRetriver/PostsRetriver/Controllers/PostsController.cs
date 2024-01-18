using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PostsRetriver.Model;
using PostsRetriver.Sevice;

namespace PostsRetriver.Controllers;
[ApiController]
[Route("[controller]")]
public class PostsController:ControllerBase
{
    private IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    /// <summary>
    /// Search articles using tags and sort them
    /// </summary>
    /// <param name="tags">Tags to be searched</param>
    /// <param name="sortBy">
    /// id 
    /// reads 
    /// likes 
    /// popularity
    /// </param>
    /// <param name="direction">
    /// asc
    /// desc
    /// </param>
    /// <returns>List of Posts</returns>
    [HttpGet]
    
    public async Task<List<Post>> GetPosts([FromQuery]List<string> tags,[FromQuery]string sortBy="id",[FromQuery]string direction="asc")
    {
        var result = await _postService.GetPosts(tags,sortBy,direction);
       
        return result;
    }

}