namespace PreSchool.Utilities.Extentions;

public static class FileCreateExtention
{
    public static string CreateFile(this IFormFile file, string webRoot, string folderName)
    {

        string filename = Guid.NewGuid().ToString() + file.FileName;
        string path = Path.Combine(webRoot, folderName, filename);
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        return filename;
    }
}
