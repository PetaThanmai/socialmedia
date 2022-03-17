using Dapper;
using Socialmedia.Models;
using Socialmedia.Utilities;
// using Hotel.Repositories;
// using Hotel.Models;
// using Hotel.Utilities;

namespace Socialmedia.Repositories;
public interface IUserRepository
{
    Task<User> Create(User Item);
    Task<bool> Update(User item);
    // Task<bool> Delete(long UserId);
    Task<User> GetById(long UserId);
    Task<List<User>> GetList();
    // Task<List<User>> GetListByUserId(long ScheduleId);
    //  Task<List<User>> GetUserByScheduleId(long UserId);
    // Task<User> GetById(long Id);
}
public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<User> Create(User item)
    {


        var query = $@"INSERT INTO ""{TableNames.user}""
        (user_id,user_name,date_of_birth,mobile,email,address,created_at)
        VALUES (@UserId,  @UserName, @DateOfBirth, @Mobile, @Email, @Address, @CreatedAt) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<User>(query, item);

            return res;
        }

    }

   

    public async Task<User> GetById(long UserId)
    {
        var query = $@"SELECT * FROM ""{TableNames.user}""
        WHERE user_id = @UserId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<User>(query, new
            {
                Userid = UserId
            });

    }

   

    public async Task<List<User>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.user}""";
        List<User> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<User>(query)).AsList();
        return res;
    }

    // public async Task<List<User>> GetUserByScheduleId(long UserId)
    // {
    //     var query = $@"SELECT * FROM ""{TableNames.user}""
    //     WHERE user_id = @UserId";
 
    //     using(var con = NewConnection){
    //        var res = (await con.QueryAsync<User>(query,new{UserId})).AsList();
    //        return res;
    //     }
    // }
    // public Task<List<UserDTO>> GetList(object UserId)
    // {

    //     return null;
    // }

    public async Task<bool> Update(User item)
    {
        var query = $@"UPDATE ""{TableNames.user}"" SET user_name=@UserName
        WHERE user_id = @UserId";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;
        }
    }

    // public async Task<User> IUserRepository.GetList()
    // {
    //     var query = $@"SELECT * FROM ""{TableNames.User}""";
    //     List<User> res;
    //     using (var con = NewConnection)
    //         res = (await con.QueryAsync<User>(query)).AsList();
    //     return res;

}

// public async Task<User> IUserRepository.GetList()
// {
//     var query = $@"SELECT * FROM ""{TableNames.User}""";
//     List<User> res;
//     using (var con = NewConnection)
//         res = (await con.QueryAsync<User>(query)).AsList();
//     return res;
// }


