using System.Collections.Generic;

namespace HRLMParser
{
    internal class TagsParser
    {
        private int _lineIndex;

        public Tag ParentTag { get; private set; }

        public void Parse(IList<TagLine> tagLines, Tag parentTag = null)
        {
            var tagLine = tagLines[_lineIndex];
            Tag tag;

            if (tagLine.IsTagEnd)
            {
                tag = parentTag != null ? parentTag.ParentTag : null;
            }
            else
            {
                tag = new Tag
                {
                    Name = tagLine.TagName,
                    TagAttribute = new TagAttribute
                    {
                        Name = tagLine.AttributeName,
                        Value = tagLine.AttributeValue
                    },
                    Tags = new List<Tag>()
                };

                if (_lineIndex == 0)
                {
                    ParentTag = tag;
                }

                if (parentTag != null)
                {
                    tag.ParentTag = parentTag;
                    parentTag.Tags.Add(tag);
                }
            }
            
            _lineIndex++;
            if (_lineIndex < tagLines.Count)
            {
                Parse(tagLines, tag);
            }
        }

        //private static void ParseTagLine(TagLine tagLine)
        //{
        //    Tag parentTag = null;

        //    foreach (var tagLine in tagLines)
        //    {
        //        parentTag = new Tag
        //        {
        //            Name = tagLine.TagName,
        //            TagAttribute = new TagAttribute
        //            {
        //                Name = tagLine.AttributeName,
        //                Value = tagLine.AttributeValue
        //            }
        //        };

        //        if (!tagLine.IsTagEnd)
        //        {

        //        }
        //    }
        //}
    }
}