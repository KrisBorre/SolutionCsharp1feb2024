namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        string catURL;
        string catJSON;

        // https://stackoverflow.com/questions/66447764/c-sharp-api-image-to-picturebox-from-url-exception-thrown

        // https://grantwinney.com/using-async-await-and-task-to-keep-the-winforms-ui-more-responsive/

        // https://free-apis.github.io/#/browse
        // https://metmuseum.github.io/

        //https://github.com/public-api-lists/public-api-lists

        // https://mixedanalytics.com/blog/list-actually-free-open-no-auth-needed-apis/

        // https://publicapis.dev/

        public Form1()
        {
            InitializeComponent();

            var client = new HttpClient();

            // ik roep await aan, maar het is geen async Task methode.

            HttpResponseMessage response = await client.GetAsync("https://api.thecatapi.com/v1/images/search");

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
