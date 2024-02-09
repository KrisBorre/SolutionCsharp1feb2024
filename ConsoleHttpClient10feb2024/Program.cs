using System.Text.RegularExpressions;

namespace ConsoleHttpClient10feb2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string catURL;
            string catJSON;
            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("https://api.thecatapi.com/v1/images/search?api_key=<apiKey>");

            HttpContent responseContent = response.Content;

            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                catJSON = (await reader.ReadToEndAsync());
            }
            dynamic items = JsonConvert.DeserializeObject(catJSON);
            foreach (var item in items)
            {
                catURL = Convert.ToString(item.url);
            }
            try
            {
                if (pictureBox1.Image != null)
                    pictureBox1.Image.Dispose();
                pictureBox1.Load(catURL);
            }
            catch { }
        }
    }
}
