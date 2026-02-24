using System;
using DapperCourseDBNormalized.Models;

namespace DapperCourseDBNormalized.Repositories;

public interface IVideoGameRepository
{
    Task<int> CreateVideoGameAsync(VideoGame videoGame);
    Task<VideoGame> GetVideoGameAsync(int id);
    Task<IEnumerable<VideoGame>> GetAllVideoGamesAsync();
    Task<int> UpdateVideoGameAsync(VideoGame videoGame);
    Task<int> DeleteVideoGameAsync(int id);

}
