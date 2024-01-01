using Dapper;
using Microsoft.Data.SqlClient;
using UploadEditDeleteImageWithDapperNetCore_Demo.Models;
using UploadEditDeleteImageWithDapperNetCore_Demo.Repositories.Interface;

namespace UploadEditDeleteImageWithDapperNetCore_Demo.Repositories.Repository
{
    public class SpeakerRepository : ISpeaker
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;

        public SpeakerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<IEnumerable<SpeakerModel>> Get()
        {
            var sql = $@"SELECT [f_uid]
                               ,[f_iid]
                               ,[f_fname]
                               ,[f_lname]
                               ,[f_gender]
                               ,[f_speach_date]
                               ,[f_speach_time]
                               ,[f_speach_venue]
                               ,[f_speaker_picture]
                               ,[f_create_date]
                               ,[f_update_date]
                               ,[f_delete_date]
                            FROM 
                                [dbo].[t_speaker]
                            WHERE
	                            [f_delete_date] IS NULL";

            return await _connection.QueryAsync<SpeakerModel>(sql);
        }

        public async Task<SpeakerModel> Find(Guid uid)
        {
            var sql = $@"SELECT [f_uid]
                               ,[f_iid]
                               ,[f_fname]
                               ,[f_lname]
                               ,[f_gender]
                               ,[f_speach_date]
                               ,[f_speach_time]
                               ,[f_speach_venue]
                               ,[f_speaker_picture]
                               ,[f_create_date]
                               ,[f_update_date]
                               ,[f_delete_date]
                          FROM 
                               [dbo].[t_speaker]
                          WHERE
                                [f_uid]=@uid AND
                                [f_delete_date] IS NULL";

            return await _connection.QueryFirstOrDefaultAsync<SpeakerModel>(sql, new { uid });
        }

        public async Task<SpeakerModel> Add(SpeakerModel model)
        {
            model.f_uid = Guid.NewGuid();
            model.f_create_date = DateTime.Now;

            var sql = $@"INSERT INTO [dbo].[t_speaker]
                               ([f_uid]
                               ,[f_fname]
                               ,[f_lname]
                               ,[f_gender]
                               ,[f_speach_date]
                               ,[f_speach_time]
                               ,[f_speach_venue]
                               ,[f_speaker_picture]
                               ,[f_create_date])
                         VALUES
                               (@f_uid,
                               @f_fname,
                               @f_lname,
                               @f_gender,
                               @f_speach_date,
                               @f_speach_time,
                               @f_speach_venue,
                               @f_speaker_picture,
                               @f_create_date)";

            await _connection.ExecuteAsync(sql, model);
            return model;
        }

        public async Task<SpeakerModel> Update(SpeakerModel model)
        {
            model.f_update_date = DateTime.Now;
            var sql = $@"UPDATE [dbo].[t_speaker]
                        SET [f_fname] = @f_fname,
                            [f_lname] = @f_lname,
                            [f_gender] = @f_gender,
                            [f_speach_date] = @f_speach_date,
                            [f_speach_time] = @f_speach_time,
                            [f_speach_venue] = @f_speach_venue,
                            [f_speaker_picture] = @f_speaker_picture,
                            [f_update_date] = @f_update_date
                        WHERE 
                            [f_uid]=@f_uid"; ;

            await _connection.ExecuteAsync(sql, model);
            return model;
        }

        public async Task<int> Remove(SpeakerModel model)
        {
            model.f_delete_date = DateTime.Now;
            var sql = $@"
                        UPDATE [t_speaker]
                        SET
                            [f_delete_date] =@f_delete_date
                        WHERE 
                            [f_uid]=@f_uid";

            return await _connection.ExecuteAsync(sql, model);
        }
    }
}
