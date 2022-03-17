using Dapper;
using Socialmedia.Models;
using Socialmedia.Utilities;
// using Hotel.Repositories;
// using Hotel.Models;
// using Hotel.Utilities;

namespace Socialmedia.Repositories;
public interface ILikeRepository
{
    Task<Like> Create(Like Item);
    Task<bool> Update(Like item);
    Task<bool> Delete(long LikeId);
    Task<Like> GetById(long LikeId);
    Task<List<Like>> GetList();
    // Task<List<Like>> GetListByLikeId(long ScheduleId);
     Task<List<Like>> GetLikeByScheduleId(long LikeId);
    // Task<Like> GetById(long Id);
}
public class LikeRepository : BaseRepository, ILikeRepository
{
    public LikeRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<Like> Create(Like item)
    {


        var query = $@"INSERT INTO ""{TableNames.like}""
        (Like_id,date_created,user_id,post_id)
        VALUES (@LikeId,  @DateCreated, @UserId, @PostId) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Like>(query, item);

            return res;
        }

    }

    public async Task<bool> Delete(long LikeId)
    {
        var query = $@"DELETE FROM ""{TableNames.like}""
        WHERE like_id = @LikeId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { LikeId });
            return res > 0;
        }
    }

    public async Task<Like> GetById(long LikeId)
    {
        var query = $@"SELECT * FROM ""{TableNames.like}""
        WHERE like_id = @LikeId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Like>(query, new
            {
                Likeid = LikeId
            });

    }

   

    public async Task<List<Like>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.like}""";
        List<Like> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Like>(query)).AsList();
        return res;
    }

    public async Task<List<Like>> GetLikeByScheduleId(long LikeId)
    {
        var query = $@"SELECT * FROM ""{TableNames.like}""
        WHERE like_id = @LikeId";
 
        using(var con = NewConnection){
           var res = (await con.QueryAsync<Like>(query,new{LikeId})).AsList();
           return res;
        }
    }
    // public Task<List<LikeDTO>> GetList(object LikeId)
    // {

    //     return null;
    // }

    public async Task<bool> Update(Like item)
    {
        var query = $@"UPDATE ""{TableNames.like}"" SET date_created=@DateCreated
        WHERE like_id = @LikeId";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;
        }
    }

    // public async Task<Like> ILikeRepository.GetList()
    // {
    //     var query = $@"SELECT * FROM ""{TableNames.Like}""";
    //     List<Like> res;
    //     using (var con = NewConnection)
    //         res = (await con.QueryAsync<Like>(query)).AsList();
    //     return res;

}

// public async Task<Like> ILikeRepository.GetList()
// {
//     var query = $@"SELECT * FROM ""{TableNames.Like}""";
//     List<Like> res;
//     using (var con = NewConnection)
//         res = (await con.QueryAsync<Like>(query)).AsList();
//     return res;
// }


