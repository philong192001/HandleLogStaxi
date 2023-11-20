namespace ManageVoyage.Models
{
    /// <summary>
    /// Khoảng dữ liệu theo ngày muốn lấy ra (chỉ cách nhau tối đa 2 ngày)
    /// </summary>
    public class UpdateDateRequest
    {
        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public decimal From { get; set; }
        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public decimal To { get; set; }
    }
}
