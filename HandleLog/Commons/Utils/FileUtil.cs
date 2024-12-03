namespace HandleLog.Commons.Utils;

public class FileUtil
{
    public static string MergeFilesInFolder(string folderPath, string outputFilePath, string controlPlate, string logInfo)
    {
        try
        {
            // Tìm tất cả các file trong thư mục với tên có liên quan (không phân biệt hoa/thường)
            //string[] files = Directory.GetFiles(folderPath, $"{controlPlate}_*.txt");
            string[] files = Directory.GetFiles(folderPath)
                                 .Where(file => Path.GetFileName(file)
                                     .StartsWith(controlPlate, StringComparison.OrdinalIgnoreCase) && file.EndsWith(".txt"))
                                 .ToArray();

            // Kiểm tra nếu không có file nào khớp
            if (files.Length == 0)
            {
                return $"No files found with prefix {controlPlate}_* in folder : {folderPath} ---";
            }

            // Tạo StreamWriter để ghi nội dung merge vào file đầu ra
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (string file in files)
                {
                    // Đọc tất cả các dòng từ file log
                    string[] lines = File.ReadAllLines(file);

                    foreach (string line in lines)
                    {
                        // Đọc tất cả các dòng từ file log
                        if (line.Contains(logInfo))
                        {
                            // Đọc tất cả các dòng từ file log
                            writer.WriteLine(line);
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