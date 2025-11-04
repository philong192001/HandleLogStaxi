using System.Text.RegularExpressions;

namespace HandleLog.Commons.Utils;

public class FileUtil
{
    public static string MergeFilesInFolder(string folderPath, string outputFilePath, string controlPlate, string logInfo)
    {
        try
        {
            // Tìm tất cả các file trong thư mục với tên có liên quan (không phân biệt hoa/thường)
            string searchPrefix = controlPlate + "_";
            string[] files = Directory.GetFiles(folderPath)
                                 .Where(file => Path.GetFileName(file)
                                     .StartsWith(searchPrefix, StringComparison.OrdinalIgnoreCase) && file.EndsWith(".txt"))
                                 .ToArray();

            // Kiểm tra nếu không có file nào khớp
            if (files.Length == 0)
            {
                return $"No files found with prefix {controlPlate}_* in folder : {folderPath} ---";
            }
            // 2. Định nghĩa Regex để tìm VehiclePlate. Coi controlPlate là BATHAO (chữ HOA)
            // và log có thể là bathao7 (chữ thường + số).
            // Biểu thức Regex: tìm "VehiclePlate":"[giá trị]"
            var regex = new Regex(@"""VehiclePlate"":""([^""]*)""", RegexOptions.IgnoreCase);

            // Tạo StreamWriter để ghi nội dung merge vào file đầu ra
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (string file in files)
                {
                    // Đọc tất cả các dòng từ file log
                    string[] lines = File.ReadAllLines(file);

                    foreach (string line in lines)
                    {
                        // Điều kiện 1: Đảm bảo dòng log có chứa logInfo
                        if (line.Contains(logInfo))
                        {
                            // Kiểm tra xem dòng log có chứa thông tin VehiclePlate không
                            if (line.ToLower().Contains("vehicleplate"))
                            {
                                // Dòng log có VehiclePlate => Áp dụng kiểm tra Regex nghiêm ngặt
                                Match match = regex.Match(line);
                                // Đảm bảo match thành công
                                if (match.Success && match.Groups.Count > 1)
                                {
                                    // Lấy giá trị VehiclePlate (Group 1 của regex)
                                    string logVehiclePlate = match.Groups[1].Value.ToLower();

                                    // Trích xuất phần tiền tố (ví dụ: "bathao" từ "bathao7")
                                    // Giả định VehiclePlate trong log bắt đầu bằng controlPlate (ví dụ: controlPlate="BATHAO", log="bathao7")
                                    // Kiểm tra nếu logVehiclePlate BẮT ĐẦU bằng controlPlate (không phân biệt hoa/thường)
                                    if (logVehiclePlate.Equals(controlPlate.ToLower()))
                                    {
                                        // Nếu khớp, ghi dòng log vào file output
                                        writer.WriteLine(line);
                                    }
                                    // Nếu không khớp (ví dụ: logVehiclePlate là "BATHAOHCM", controlPlate là "BATHAO"), sẽ bị bỏ qua.
                                }

                            }
                            else
                            {
                                // Dòng log KHÔNG chứa VehiclePlate (ví dụ: các thông báo/lỗi khác, như dòng bạn khoanh đỏ)
                                // => Chỉ cần thỏa mãn logInfo là ghi vào file
                                writer.WriteLine(line);
                            }
                        }
                    }
                }
            }
            return $"Merge file prefix {controlPlate}_* done in folder : {folderPath} ---";
        }
        catch (Exception ex)
        {
            return null;
            //ex.Message;
        }
    }
}