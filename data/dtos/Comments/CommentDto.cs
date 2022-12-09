using Bookie.data.entities;
namespace Bookie.data.dtos.Comments
{
    public record CommentDto(int Id, int BookID, DateTime Date, string Text,string UserId, string Username);
    public record CreateCommentDto(string Text);
    public record UpdateCommentDto(string Text);

}
