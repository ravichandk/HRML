using System;
using System.Collections.Generic;

namespace HRLMParser
{
    internal class Program
    {
        static void Main()
        {
            var testCase = new TestCase2();

            var linesToParse = testCase.LinesToParse;
            var queriesToParse = testCase.QueriesToParse;

            var tagLines = new TagLineParser().Parse(linesToParse);
            var tagParser = new TagsParser();
            tagParser.Parse(tagLines);

            var parentTag = tagParser.ParentTag;

            //var query = "tag1.tag2.tag3.tag5~name";

            foreach (var query in queriesToParse)
            {
                QueryEvaluator.Evaluate(query, parentTag);
            }

            Console.ReadLine();
        }
    }

    internal class TagLine
    {
        public string TagName { get; set; }

        public string AttributeName { get; set; }

        public string AttributeValue { get; set; }

        public bool IsTagEnd { get; set; }

        public IList<string> InnerTagLines { get; set; }
    }

    internal class Tag
    {
        public string Name { get; set; }

        public TagAttribute TagAttribute { get; set; }

        public IList<Tag> Tags { get; set; }

        public Tag ParentTag { get; set; }
    }

    internal class TagAttribute
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
