using MongoDB.Bson;

namespace ChangeDB.Models.v2;

public class BaseEntityMongo
{
    /// <summary>
    /// Id mongoDB
    /// </summary>
    public BsonObjectId _id { get; set; }
    /// <summary>
    /// Ngày tạo
    /// </summary>
    public double CreatedDate { get; set; }
    /// <summary>
    /// Ngày giờ cập nhật
    /// </summary>

    public double? UpdatedDate { get; set; }
    /// <summary>
    /// Người tạo
    /// </summary>

    public Guid? CreatedByUser { get; set; }
    /// <summary>
    /// Người cập nhật
    /// </summary>

    public Guid? UpdatedByUser { get; set; }
    /// <summary>
    /// Xóa record
    /// </summary>
    public bool IsDeleted { get; set; }
    /// <summary>
    /// ID công ty
    /// </summary>

    public int? CompanyId { get; set; }
}
