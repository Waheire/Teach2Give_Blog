using Post_Service.Models;

namespace Post_Service.Services.IService
{
    public interface IPost
    {
        Task<List<Post>> GetAllPostsAsync();

        Task<Post> GetPostByIdAsync(Guid id);
        Task<List<Post>> GetPostOfUser(Guid userId);

        Task<string> AddPostAsync(Post post);
    }
}
