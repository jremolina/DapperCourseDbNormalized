using System;

namespace DapperCourseDBNormalized.Models;

public class Platform
{
    public int Id { get; set; }
    public required string Name { get; set; }

    // Navigation property
    public List<VideoGame> VideoGames { get; set; } = [];

}
