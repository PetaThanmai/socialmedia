using Dapper;
using Socialmedia.Models;
using Socialmedia.Utilities;
// using Hotel.Repositories;
// using Hotel.Models;
// using Hotel.Utilities;

namespace Socialmedia.Repositories;
public interface IPostRepository
{
    Task<Post> Create(Post Item);
    Task<bool> Update(Post item);
    Task<bool> Delete(long PostId);
    Task<Post> GetById(long PostId);
    Task<List<Post>> GetList();
    Task<IList<Post>> GetListPostById(long user_id);
    Task<List<Post>> GetPostsByHashId(long HashId);
    //  Task<List<Post>> GetPostByScheduleId(long PostId);
    // Task<Post> GetById(long Id);
}
public class PostRepository : BaseRepository, IPostRepository
{
    public PostRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<Post> Create(Post item)
    {


        var query = $@"INSERT INTO ""{TableNames.post}""
        (post_id,post_type,date_created,date_updated,user_id)
        VALUES (@PostId,  @PostType, @DateCreated, @DateUpdated,@UserId) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Post>(query, item);

            return res;
        }

    }

    public async Task<bool> Delete(long PostId)
    {
        var query = $@"DELETE FROM ""{TableNames.post}""
        WHERE post_id = @PostId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { PostId });
            return res > 0;
        }
    }

    public async Task<Post> GetById(long PostId)
    {
        var query = $@"SELECT * FROM ""{TableNames.post}""
        WHERE post_id = @PostId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Post>(query, new
            {
                Postid = PostId
            });

    }

   

    public async Task<List<Post>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.post}""";
        List<Post> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Post>(query)).AsList();
        return res;
    }

    public async Task<IList<Post>> GetListPostById(long user_id)
    {
        var query =$@"SELECT * FROM ""{TableNames.post}""";
        List<Post> res;
        using(var con=NewConnection)
              res=(await con.QueryAsync<Post>(query, new {user_id})).AsList();
            return res;
    }

    

    public async Task<List<Post>> GetPost(long PostId)
    {
        var query = $@"SELECT * FROM ""{TableNames.post}""
        WHERE post_id = @PostId";
 
        using(var con = NewConnection){
           var res = (await con.QueryAsync<Post>(query,new{PostId})).AsList();
           return res;
        }
    }

    public async Task<List<Post>> GetPostsByHashId(long HashId)
    {
        var query = $@"SELECT p.* FROM ""{TableNames.post_hash}"" ph  LEFT JOIN ""{TableNames.post}"" p ON p.post_id = ph.post_id WHERE hash_id = @HashId";
 
        using(var con = NewConnection){
           var res = (await con.QueryAsync<Post>(query,new{HashId})).AsList();
        return res;
        }

    // public Task<List<Post>> GetPostByScheduleId(long PostId)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<List<PostDTO>> GetList(object PostId)
    // {

    //     return null;
    // }


    

    }

    public async Task<bool> Update(Post item)
    {
        var query = $@"UPDATE ""{TableNames.post}"" SET post_type=@PostType,
        WHERE post_id = @PostId";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;
        }
    }
}
//     public async Task<List<Post>> IPostRepository.GetList()
//     {
//         var query = $@"SELECT * FROM ""{TableNames.post}""";
//         List<Post> res;
//         using (var con = NewConnection)
//             res = (await con.QueryAsync<Post>(query)).AsList();
//         return res;

// }
// }

//     public Task<List<Post>> GetPostByScheduleId(long PostId)
//     {
//         throw new NotImplementedException();
//     }
// }

// public async Task<Post> IPostRepository.GetList()
// {
//     var query = $@"SELECT * FROM ""{TableNames.post}""";
//     List<Post> res;
//     using (var con = NewConnection)
//         res = (await con.QueryAsync<Post>(query)).AsList();
//     return res;
// }


