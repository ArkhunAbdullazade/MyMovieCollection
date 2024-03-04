using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovieCollection.Core.Models;
public class UserUser
{
    public int Id { get; set; }

    [ForeignKey("FolowerId")]
    public string? FollowerId { get; set; }
    public User? Follower { get; set; }

    [ForeignKey("FolowedUserId")]
    public string? FollowedUserId { get; set; }
    public User? FollowedUser { get; set; }
}
