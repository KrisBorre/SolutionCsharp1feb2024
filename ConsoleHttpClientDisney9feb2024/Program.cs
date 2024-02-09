namespace ConsoleHttpClientDisney9feb2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://disneyapi.dev/docs/

            using var client = new HttpClient();

            string requestUri = "https://api.disneyapi.dev/character?name=Bambi";

            HttpResponseMessage response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var resp = await response.Content.ReadAsStringAsync();

            await Console.Out.WriteLineAsync(resp);

            /*{"info":{"count":2,"totalPages":1,"previousPage":null,"nextPage":null},"data":[{"_id":455,"films":["Bambi (film)","Bambi II","Perri","The Rescuers","Who Framed Roger Rabbit","Mickey's Magical Christmas: Snowed in at the House of Mouse","A Goofy Movie","The Reluctant Dragon","Zootopia","Ralph Breaks the Internet","Bambi (live-action film)"],"shortFilms":["No Hunting"],"tvShows":["House of Mouse","A Poem Is...","Mickey Mouse (TV series)"],"videoGames":["Kingdom Hearts (series)","Disney Tsum Tsum (game)","Disney Emoji Blitz","Disney Magic Kingdoms"],"parkAttractions":["It's a Small World","Fantasmic!","World of Color","Celebrate the Magic","Main Street Electrical Parade","Jingle Bell, Jingle BAM!"],"allies":[],"enemies":[],"sourceUrl":"https://disney.fandom.com/wiki/Bambi_(character)","name":"Bambi","imageUrl":"https://static.wikia.nocookie.net/disney/images/c/ce/Profile_-_Bambi.png","createdAt":"2021-04-12T01:35:05.218Z","updatedAt":"2021-12-20T20:39:21.786Z","url":"https://api.disneyapi.dev/characters/455","__v":0},{"_id":456,"films":["Bambi (film)","The Sword in the Stone","The Jungle Book","The Rescuers","Who Framed Roger Rabbit","Beauty and the Beast (1991 film)","Bambi II"],"shortFilms":["No Hunting"],"tvShows":["A Poem Is..."],"videoGames":[],"parkAttractions":["World of Color"],"allies":[],"enemies":[],"sourceUrl":"https://disney.fandom.com/wiki/Bambi%27s_mother","name":"Bambi's mother","imageUrl":"https://static.wikia.nocookie.net/disney/images/f/fd/Bambi%27s_mother.jpg","createdAt":"2021-04-12T01:35:06.131Z","updatedAt":"2021-12-20T20:39:21.786Z","url":"https://api.disneyapi.dev/characters/456","__v":0}]}*/

            Console.Read();
        }
    }
}
