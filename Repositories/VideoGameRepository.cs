using System;
using Dapper;
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

    public async Task<IEnumerable<VideoGame>> GetAllVideoGamesAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sql = @"select vg.*, p.*,d.*,gd.*,r.*,pf.*
                        from VideoGames vg
                        left join Publishers p on vg.PublisherId = p.Id
                        left join Developers d on vg.DeveloperId = d.Id
                        left join GameDetails gd on vg.Id = gd.VideoGameId
                        left join Reviews r on vg.Id = r.VideoGameId
                        left join VideoGamesPlatforms vgp on vg.Id = vgp.VideoGameId
                        left join Platforms pf on pf.id = vgp.PlatformId";

            var videoGameDictionary = new Dictionary<int, VideoGame>();
            var games = await connection.QueryAsync<VideoGame, Publisher, Developer, GameDetail, Review, Platform, VideoGame>(
                sql,
                (videoGame, publisher, developer, gameDetail, review, platform) =>
                {
                    if (!videoGameDictionary.TryGetValue(videoGame.Id, out var currentGame))
                    {
                        currentGame = videoGame;
                        currentGame.Publisher = publisher;
                        currentGame.Developer = developer;
                        currentGame.GameDetail = gameDetail;
                        currentGame.Reviews = new List<Review>();
                        currentGame.Platforms = new List<Platform>();
                        videoGameDictionary.Add(currentGame.Id, currentGame);
                    }

                    if (review is not null && !currentGame.Reviews.Any(r => r.Id == review.Id))
                    {
                        currentGame.Reviews.Add(review);
                    }
                    
                    if (platform is not null && !currentGame.Platforms.Any(p => p.Id == platform.Id))
                    {
                        currentGame.Platforms.Add(platform);
                    }

                    return currentGame;
                }, splitOn: "Id,Id,VideoGameId, Id,Id"
                
            );
            return videoGameDictionary.Values;
        }
    }


    public async Task<VideoGame> GetVideoGameAsync(int id)
    {
                using (var connection = new SqlConnection(_connectionString))
        {
            var sql = @"select vg.*, p.*,d.*,gd.*,r.*,pf.*
                        from VideoGames vg
                        left join Publishers p on vg.PublisherId = p.Id
                        left join Developers d on vg.DeveloperId = d.Id
                        left join GameDetails gd on vg.Id = gd.VideoGameId
                        left join Reviews r on vg.Id = r.VideoGameId
                        left join VideoGamesPlatforms vgp on vg.Id = vgp.VideoGameId
                        left join Platforms pf on pf.id = vgp.PlatformId where vg.Id = @id";

            var videoGameDictionary = new Dictionary<int, VideoGame>();
            var games = await connection.QueryAsync<VideoGame, Publisher, Developer, GameDetail, Review, Platform, VideoGame>(
                sql,
                (videoGame, publisher, developer, gameDetail, review, platform) =>
                {
                    if (!videoGameDictionary.TryGetValue(videoGame.Id, out var currentGame))
                    {
                        currentGame = videoGame;
                        currentGame.Publisher = publisher;
                        currentGame.Developer = developer;
                        currentGame.GameDetail = gameDetail;
                        currentGame.Reviews = new List<Review>();
                        currentGame.Platforms = new List<Platform>();
                        videoGameDictionary.Add(currentGame.Id, currentGame);
                    }

                    if (review is not null && !currentGame.Reviews.Any(r => r.Id == review.Id))
                    {
                        currentGame.Reviews.Add(review);
                    }
                    
                    if (platform is not null && !currentGame.Platforms.Any(p => p.Id == platform.Id))
                    {
                        currentGame.Platforms.Add(platform);
                    }

                    return currentGame;
                }, 
                new {Id = id},
                splitOn: "Id,Id,VideoGameId, Id,Id"
                
            );
            return videoGameDictionary.Values.FirstOrDefault();
        }
    }

    public Task<int> UpdateVideoGameAsync(VideoGame videoGame)
    {
        throw new NotImplementedException();
    }
}
