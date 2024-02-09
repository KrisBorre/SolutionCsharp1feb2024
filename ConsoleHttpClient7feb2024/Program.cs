using System.Net.Http.Headers;

namespace ConsoleHttpClient7feb2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://zetcode.com/csharp/httpclient/
            Console.WriteLine("HttpClient is a base class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.");

            Console.WriteLine("JSON (JavaScript Object Notation) is a lightweight data-interchange format. " +
                "This format is easy for humans to read and write and for machines to parse and generate. " +
                "It is a less verbose and more readable alternative to XML. " +
                "The official Internet media type for JSON is application/json.");

            using var client = new HttpClient();

            client.BaseAddress = new Uri("https://api.github.com");
            client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Symfony is a PHP framework for web and console applications and a set of reusable PHP components.
            // Symfony is used by thousands of web applications and most of the popular PHP projects.
            // Symfony has 2930 contributors

            //var url = "repos/symfony/symfony/contributors";
            //var url = "repos/KrisBorre/SolutionCsharp11aug2023/issues";
            var url = "repos/KrisBorre/SolutionCsharp11aug2023";

            /*
            https://docs.github.com/en/rest/using-the-rest-api/getting-started-with-the-rest-api?apiVersion=2022-11-28
            Each endpoint has a path. 
            The REST API reference documentation gives the path for every endpoint. 
            For example, the path for the "List repository issues" endpoint is /repos/{owner}/{repo}/issues.
            The curly brackets {} in a path denote path parameters that you need to specify. 
            Path parameters modify the endpoint path and are required in your request. 
            For example, the path parameters for the "List repository issues" endpoint are {owner} and {repo}. 
            To use this path in your API request, replace {repo} with the name of the repository where you would like to request a list of issues, and replace {owner} with the name of the account that owns the repository.
            */

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();

            await Console.Out.WriteLineAsync(resp);

            /*{"id":677386303,"node_id":"R_kgDOKGAYPw","name":"SolutionCsharp11aug2023","full_name":"KrisBorre/SolutionCsharp11aug2023","private":false,"owner":{"login":"KrisBorre","id":135237046,"node_id":"U_kgDOCA-Ntg","avatar_url":"https://avatars.githubusercontent.com/u/135237046?v=4","gravatar_id":"","url":"https://api.github.com/users/KrisBorre","html_url":"https://github.com/KrisBorre","followers_url":"https://api.github.com/users/KrisBorre/followers","following_url":"https://api.github.com/users/KrisBorre/following{/other_user}","gists_url":"https://api.github.com/users/KrisBorre/gists{/gist_id}","starred_url":"https://api.github.com/users/KrisBorre/starred{/owner}{/repo}","subscriptions_url":"https://api.github.com/users/KrisBorre/subscriptions","organizations_url":"https://api.github.com/users/KrisBorre/orgs","repos_url":"https://api.github.com/users/KrisBorre/repos","events_url":"https://api.github.com/users/KrisBorre/events{/privacy}","received_events_url":"https://api.github.com/users/KrisBorre/received_events","type":"User","site_admin":false},"html_url":"https://github.com/KrisBorre/SolutionCsharp11aug2023","description":null,"fork":false,"url":"https://api.github.com/repos/KrisBorre/SolutionCsharp11aug2023","forks_url":"https://api.github.com/repos/KrisBorre/SolutionCsharp11aug2023/forks","keys_url":"https://api.github.com/repos/KrisBorre/SolutionCsharp11aug2023/keys{/key_id}","collaborators_url":"https://api.github.com/repos/KrisBorre/SolutionCsharp11aug2023/collaborators{/collaborator}","teams_url":"https://api.github.com/repos/KrisBorre/SolutionCsharp11aug2023/teams","hooks_url":"https://api.github.com/repos/KrisBorre/SolutionCsharp11aug2023/hooks","issue_events_url":"https://api.github.com/repos/KrisBorre/SolutionCsharp11aug2023/issues/events{/number}"*/

            Console.Read();
        }
    }
}
