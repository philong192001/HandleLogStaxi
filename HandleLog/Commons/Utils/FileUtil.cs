namespace HandleLog.Commons.Utils;

public class FileUtil
{
    public static string MergeFilesInFolder(string folderPath, string outputFilePath, string controlPlate, string logInfo)
    {
        try
        {
            // Get all files in the folder with the specified pattern
            string[] files = Directory.GetFiles(folderPath, $"{controlPlate}_*.txt");

            // Create a StreamWriter to write the merged content to the output file
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (string file in files)
                {
                    // Read all lines from the log file
                    string[] lines = File.ReadAllLines(file);

                    foreach (string line in lines)
                    {
                        // Check if the log line contains the specified log level
                        if (line.Contains(logInfo))
                        {
                            // Write the log line to the filtered log file
                            writer.WriteLine(line);
                        }
                    }
                }
            }
            return $"Merge file prefix {controlPlate}_* done in folder : {folderPath}   ";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}