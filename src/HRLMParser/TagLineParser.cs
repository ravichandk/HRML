using System;
using System.Collections.Generic;
using System.Linq;

namespace HRLMParser
{
    internal class TagLineParser
    {
        public IList<TagLine> Parse(IList<string> lines)
        {
            var tagLines = new List<TagLine>();

            if (lines.Count <= 0) return tagLines;

            foreach (var eachline in lines)
            {
                var line = eachline.Trim();

                if ((!line.StartsWith("<") || !line.EndsWith(">")) && (!line.StartsWith("</") || !line.EndsWith(">")))
                    continue;

                if (line.StartsWith("</"))
                {
                    tagLines.Add(new TagLine { TagName = line.Replace("</", "").Replace(">", ""), IsTagEnd = true });
                    continue;
                }

                if (!line.Contains(" ")) continue;

                var firstIndexOfSpace = line.IndexOf(" ", StringComparison.Ordinal);
                var tagName = line.Substring(line.IndexOf("<", StringComparison.Ordinal) + 1, firstIndexOfSpace);

                var attribute = line.Substring(firstIndexOfSpace, line.Length - firstIndexOfSpace - 1);
                var tagLine = new TagLine { TagName = tagName.Trim() };

                if (string.IsNullOrWhiteSpace(attribute))
                {
                    tagLines.Add(tagLine);
                    continue;
                }

                if (!attribute.Contains("="))
                {
                    tagLines.Add(tagLine);
                    continue;
                }

                var attributeSplit = attribute.Split('=');
                if (attributeSplit.Length <= 1)
                {
                    tagLines.Add(tagLine);
                    continue;
                }

                tagLine.AttributeName = string.IsNullOrWhiteSpace(attributeSplit[0]) ? null : attributeSplit[0].Trim();
                tagLine.AttributeValue = string.IsNullOrWhiteSpace(attributeSplit[1])
                    ? null
                    : attributeSplit[1].Trim().Replace("'", "").Replace(@"""", "");

                tagLines.Add(tagLine);
            }

            return tagLines;
        }
    }
}