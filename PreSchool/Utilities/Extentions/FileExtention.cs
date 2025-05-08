    namespace PreSchool.Utilities.Extentions;

    public static class FileExtention
    {
        public static string CreateFile(this IFormFile file, string webRoot, string folderName)
        {

            string filename = Guid.NewGuid().ToString() + file.FileName;
            string path = Path.Combine(webRoot, folderName, filename);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filename;
        }

    public static void RemoveFile(string webRoot, string folderName, string fileName)
    {
        string path = Path.Combine(webRoot, folderName, fileName);
        if (File.Exists(path)) File.Delete(path);
    }
}
