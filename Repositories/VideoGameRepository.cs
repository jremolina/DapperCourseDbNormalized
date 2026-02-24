using System;
using DapperCourseDBNormalized.Models;
using Microsoft.Data.SqlClient;

namespace DapperCourseDBNormalized.Repositories;

public class VideoGameRepository : IVideoGameRepository
{
    private readonly string _connectionString;

    public VideoGameRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public Task<int> CreateVideoGameAsync(VideoGame videoGame)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteVideoGameAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<VideoGame>> GetAllVideoGamesAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {}
    }

    public Task<VideoGame> GetVideoGameAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateVideoGameAsync(VideoGame videoGame)
    {
        throw new NotImplementedException();
    }
}
