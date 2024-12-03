using ChangeDB.Enums;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace ChangeDB.Models.v2;

/// <summary>
/// Thông báo lặp từ thông báo master
/// </summary>
public class ArticleRepeat : ArticleMaster
{
    /// <summary>
    /// Id cha của thông báo lặp
    /// </summary>
    public BsonObjectId? ParentArticleId { get; set; }

    /// <summary>
    /// Token firebase của LX
    /// </summary>
    public string FirebaseToken { get; set; }

    /// <summary>
    /// Trạng thái gửi thông báo
    /// </summary>
    public ArticleSentStatusEnum SentStatus { get; set; } = ArticleSentStatusEnum.Created;

    /// <summary>
    /// Trạng thái đọc thông báo
    /// </summary>
    public bool IsRead { get; set; }

    [JsonIgnore]
    public string? Link { get; set; }
}
