using System.Collections.Generic;
using System.Linq;

namespace HRLMParser
{
    internal abstract class TestCase
    {
        public string Text { get; set; }
        public int NoOfLinesToParse { get; set; }
        public int NoOfQueryLines { get; set; }

        protected TestCase(string text, int noOfLinesToParse, int noOfQueryLines)
        {
            Text = text;
            NoOfLinesToParse = noOfLinesToParse;
            NoOfQueryLines = noOfQueryLines;
        }

        private IList<string> Lines
        {
            get
            {
                var lines = new List<string>();
                if (string.IsNullOrWhiteSpace(Text)) return lines;

                var text = Text.Replace("\r\n", "\n").Replace("\t", "");
                var linesSplit = text.Split('\n').ToList();
                linesSplit.ForEach(l => lines.Add(l.Trim()));
                return lines; 
            }
        }

        public IList<string> LinesToParse
        {
            get
            {
                if (Lines.Count >= NoOfLinesToParse)
                {
                    return Lines.Take(NoOfLinesToParse).ToList();
                }

                return new List<string>();
            }
        }

        public IList<string> QueriesToParse
        {
            get
            {
                if (Lines.Count >= NoOfLinesToParse + NoOfQueryLines)
                {
                    return Lines.Skip(NoOfLinesToParse).Take(NoOfQueryLines).ToList();
                }

                return new List<string>();
            }
        }
    }

    internal class TestCase1 : TestCase
    {
        private const string _text = @"<tag1 value = 'HelloWorld'>
                                            <tag2 name = 'Name2'>
                                            </tag2>
                                        </tag1>
                                        tag1.tag2~name
                                        tag1~name
                                        tag1~value";

        public TestCase1()
            : base(_text, 12, 7)
        {
        }
    }

    internal class TestCase2 : TestCase
    {
        private const string _text = @"<tag1 value = 'HelloWorld'>
	                                <tag2 name = 'Name2'>
	                                </tag2>
	                                <tag3 name = 'Name3'>
		                                <tag4 name='Name4'>
			                                <tag5 name='Name5'>
			                                </tag5>
		                                </tag4>
		                                <tag6 name='Name6'>
		                                </tag6>
	                                </tag3>
                                </tag1>
                                tag1.tag2~name
                                tag1~name
                                tag1.tag3~name
                                tag1.tag3.tag4~name
                                tag1.tag3.tag4.tag5~name
                                tag1.tag3.tag6~name
                                tag1.tag2.tag3.tag5~name
                                tag1.tag3.tag6~value";

        public TestCase2()
            : base(_text, 12, 7)
        {
        }
    }
}