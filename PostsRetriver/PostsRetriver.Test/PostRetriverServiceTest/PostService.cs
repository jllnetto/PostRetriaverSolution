using PostsRetriver.Sevice;
using Xunit;
using Moq;
using PostsRetriver.Communication;

namespace PostsRetriver.Test.PostRetriverServiceTest;

public class PostService
{
    private Mock<IPostService> _postService;
    private Mock<IPostCommunicator> _postCommunicator;
    private IPostService CreatePostServiceInstance()
    {
        _postCommunicator = new Mock<IPostCommunicator>();

        return new Sevice.PostService(_postCommunicator.Object);
    }

    [Fact]
    public void Should_Not_Validate_If_Tags_Is_Null()
    {
        var service = CreatePostServiceInstance();

        var exception = Assert.Throws<ApplicationException>(()=>service.ValidadePatamether(null, "id", "desc"));
        Assert.Equal("Tags are invalid",exception.Message);
    }

    [Fact]
    public void Should_Not_Validate_If_SortBy_Is_Null()
    {
        var service = CreatePostServiceInstance();
        List<string> tags = new List<string>();
        tags.Add("tech");
        var exception = Assert.Throws<ApplicationException>(()=>service.ValidadePatamether(tags, null,"desc"));
        Assert.Equal("SortBy is Invalid",exception.Message);
    }
    [Fact]
    public void Should_Not_Validate_If_Direction_Is_Null()
    {
        var service = CreatePostServiceInstance();
        List<string> tags = new List<string>();
        tags.Add("tech");
        var exception = Assert.Throws<ApplicationException>(()=>service.ValidadePatamether(tags, "id",null));
        Assert.Equal("Direction is invalid",exception.Message);
    }
    
    [Fact]
    public void Should_Validate_If_All_The_Paramethers_Are_Correct()
    {
        var service = CreatePostServiceInstance();
        List<string> tags = new List<string>();
        tags.Add("tech");
       Assert.True(service.ValidadePatamether(tags, "id","desc"));
    }
}