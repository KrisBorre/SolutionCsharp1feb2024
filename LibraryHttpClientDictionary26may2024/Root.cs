﻿namespace LibraryHttpClientDictionary26may2024
{
    // https://json2csharp.com/
    // var myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);

    public class Definition
    {
        public string definition { get; set; }
        public List<object> synonyms { get; set; }
        public List<object> antonyms { get; set; }
        public string example { get; set; }
    }

    public class License
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Meaning
    {
        public string partOfSpeech { get; set; }
        public List<Definition> definitions { get; set; }
        public List<string> synonyms { get; set; }
        public List<string> antonyms { get; set; }
    }

    public class Phonetic
    {
        public string audio { get; set; }
        public string sourceUrl { get; set; }
        public License license { get; set; }
        public string text { get; set; }
    }

    public class Root
    {
        public string word { get; set; }
        public List<Phonetic> phonetics { get; set; }
        public List<Meaning> meanings { get; set; }
        public License license { get; set; }
        public List<string> sourceUrls { get; set; }
    }


}
