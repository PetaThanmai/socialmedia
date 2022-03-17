using Dapper;
using Socialmedia.Models;
using Socialmedia.Utilities;
// using Hotel.Repositories;
// using Hotel.Models;
// using Hotel.Utilities;

namespace Socialmedia.Repositories;
public interface IHashRepository
{
    Task<Hash> Create(Hash Item);
    Task<bool> Update(Hash item);
    Task<bool> Delete(long HashId);
    Task<Hash> GetById(long HashId);
    Task<List<Hash>> GetList();
    Task<List<Hash>> GetListByPostId(long PostId);
    //  Task<List<Hash>> GetHashByScheduleId(long HashId);
    // Task<Hash> GetById(long Id);
}
public class HashRepository : BaseRepository, IHashRepository
{
    public HashRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<Hash> Create(Hash item)
    {


        var query = $@"INSERT INTO ""{TableNames.hash}""
        (hash_id,hash_name,)
        VALUES (@HashId,  @HashName) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Hash>(query, item);

            return res;
        }

    }

    public async Task<bool> Delete(long HashId)
    {
        var query = $@"DELETE FROM ""{TableNames.hash}""
        WHERE hash_id = @HashId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { HashId });
            return res > 0;
        }
    }

    public async Task<Hash> GetById(long HashId)
    {
        var query = $@"SELECT * FROM ""{TableNames.hash}""
        WHERE hash_id = @HashId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Hash>(query, new
            {
                Hashid = HashId
            });

    }

   

    public async Task<List<Hash>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.hash}""";
        List<Hash> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Hash>(query)).AsList();
        return res;
    }

    public async Task<List<Hash>> GetHashByScheduleId(long HashId)
    {
        var query = $@"SELECT * FROM ""{TableNames.hash}""
        WHERE hash_id = @HashId";
 
        using(var con = NewConnection){
           var res = (await con.QueryAsync<Hash>(query,new{HashId})).AsList();
           return res;
        }
    }
    // public Task<List<HashDTO>> GetList(object HashId)
    // {

    //     return null;
    // }

    public async Task<bool> Update(Hash item)
    {
        var query = $@"UPDATE ""{TableNames.hash}"" SET hash_name=@hashName
        WHERE hash_id = @HashId";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;
        }
    }

    public  async Task<List<Hash>> GetListByPostId(long PostId)
    {
    // var query = $@"SELECT * FROM ""{TableNames.hash}""
    //     WHERE hash_id = @HashId";

       var query = $@"SELECT * FROM ""{TableNames.post_hash}"" ph  
       LEFT JOIN ""{TableNames.hash}"" h ON h.hash_id = ph.hash_id 
       WHERE ph.post_id = @PostId";
 
 
        using(var con = NewConnection){
           var res = (await con.QueryAsync<Hash>(query,new{PostId})).AsList();
           return res;
        }
    }
}
        
    

    // public async Task<Hash> IHashRepository.GetList()
    // {
    //     var query = $@"SELECT * FROM ""{TableNames.Hash}""";
    //     List<Hash> res;
    //     using (var con = NewConnection)
    //         res = (await con.QueryAsync<Hash>(query)).AsList();
    //     return res;



// public async Task<Hash> IHashRepository.GetList()
// {
//     var query = $@"SELECT * FROM ""{TableNames.Hash}""";
//     List<Hash> res;
//     using (var con = NewConnection)
//         res = (await con.QueryAsync<Hash>(query)).AsList();
//     return res;
// }


