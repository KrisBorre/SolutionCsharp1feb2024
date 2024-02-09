namespace ConsoleHttpClientTheCatApi9feb2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var httpClient = new HttpClient();

            var url = "https://api.thecatapi.com/v1/images/search";
            byte[] imageBytes = await httpClient.GetByteArrayAsync(url);

            string documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string localFilename = "image6432.jpg";
            string localPath = Path.Combine(documentsPath, localFilename);

            Console.WriteLine(localPath);
            File.WriteAllBytes(localPath, imageBytes);

            Console.Read();
        }
    }
}
