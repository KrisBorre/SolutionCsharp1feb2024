// https://json2csharp.com/code-converters/xml-to-csharp and kick Latex code out.

// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(Feed));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (Feed)serializer.Deserialize(reader);
// }

using System.Xml.Serialization;

namespace ConsoleHttpClientArxiv21may2024
{
    [XmlRoot(ElementName = "link")]
    public class Link
    {

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }

        [XmlAttribute(AttributeName = "rel")]
        public string Rel { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }
    }

    [XmlRoot(ElementName = "title")]
    public class Title
    {

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "id")]
    public class Id
    {

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "updated")]
    public class Updated
    {

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlText]
        public DateTime Text { get; set; }
    }

    [XmlRoot(ElementName = "totalResults")]
    public class TotalResults
    {

        [XmlAttribute(AttributeName = "opensearch")]
        public string Opensearch { get; set; }

        [XmlText]
        public int Text { get; set; }
    }

    [XmlRoot(ElementName = "startIndex")]
    public class StartIndex
    {

        [XmlAttribute(AttributeName = "opensearch")]
        public string Opensearch { get; set; }

        [XmlText]
        public int Text { get; set; }
    }

    [XmlRoot(ElementName = "itemsPerPage")]
    public class ItemsPerPage
    {

        [XmlAttribute(AttributeName = "opensearch")]
        public string Opensearch { get; set; }

        [XmlText]
        public int Text { get; set; }
    }

    [XmlRoot(ElementName = "published")]
    public class Published
    {

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlText]
        public DateTime Text { get; set; }
    }

    [XmlRoot(ElementName = "summary")]
    public class Summary
    {

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "name")]
    public class Name
    {

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "author")]
    public class Author
    {

        [XmlElement(ElementName = "name")]
        public Name Name { get; set; }

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "comment")]
    public class Comment
    {

        [XmlAttribute(AttributeName = "arxiv")]
        public string Arxiv { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "journal_ref")]
    public class JournalRef
    {

        [XmlAttribute(AttributeName = "arxiv")]
        public string Arxiv { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "primary_category")]
    public class PrimaryCategory
    {

        [XmlAttribute(AttributeName = "arxiv")]
        public string Arxiv { get; set; }

        [XmlAttribute(AttributeName = "term")]
        public string Term { get; set; }

        [XmlAttribute(AttributeName = "scheme")]
        public string Scheme { get; set; }
    }

    [XmlRoot(ElementName = "category")]
    public class Category
    {

        [XmlAttribute(AttributeName = "term")]
        public string Term { get; set; }

        [XmlAttribute(AttributeName = "scheme")]
        public string Scheme { get; set; }
    }

    [XmlRoot(ElementName = "entry")]
    public class Entry
    {

        [XmlElement(ElementName = "id")]
        public Id Id { get; set; }

        [XmlElement(ElementName = "published")]
        public Published Published { get; set; }

        [XmlElement(ElementName = "updated")]
        public Updated Updated { get; set; }

        [XmlElement(ElementName = "title")]
        public Title Title { get; set; }

        [XmlElement(ElementName = "summary")]
        public Summary Summary { get; set; }

        [XmlElement(ElementName = "author")]
        public Author Author { get; set; }

        [XmlElement(ElementName = "comment")]
        public Comment Comment { get; set; }

        [XmlElement(ElementName = "journal_ref")]
        public JournalRef JournalRef { get; set; }

        [XmlElement(ElementName = "link")]
        public List<Link> Link { get; set; }

        [XmlElement(ElementName = "primary_category")]
        public PrimaryCategory PrimaryCategory { get; set; }

        [XmlElement(ElementName = "category")]
        public Category Category { get; set; }

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlAttribute(AttributeName = "arxiv")]
        public string Arxiv { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "feed")]
    public class Feed
    {

        [XmlElement(ElementName = "link")]
        public Link Link { get; set; }

        [XmlElement(ElementName = "title")]
        public Title Title { get; set; }

        [XmlElement(ElementName = "id")]
        public Id Id { get; set; }

        [XmlElement(ElementName = "updated")]
        public Updated Updated { get; set; }

        [XmlElement(ElementName = "totalResults")]
        public TotalResults TotalResults { get; set; }

        [XmlElement(ElementName = "startIndex")]
        public StartIndex StartIndex { get; set; }

        [XmlElement(ElementName = "itemsPerPage")]
        public ItemsPerPage ItemsPerPage { get; set; }

        [XmlElement(ElementName = "entry")]
        public Entry Entry { get; set; }

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlAttribute(AttributeName = "opensearch")]
        public string Opensearch { get; set; }

        [XmlAttribute(AttributeName = "arxiv")]
        public string Arxiv { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

}