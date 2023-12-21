using Microsoft.EntityFrameworkCore;
using Post_Service.Data;
using Post_Service.Models;
using Post_Service.Services.IService;

namespace Post_Service.Services
{
    public class PostService : IPost
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddPostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return "Post Added successfully";
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
          return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            return await _context.Posts.Where(x => x.PostId == id).FirstOrDefaultAsync();
        }

        public async Task<List<Post>> GetPostOfUser(Guid userId)
        {
            return await _context.Posts.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
