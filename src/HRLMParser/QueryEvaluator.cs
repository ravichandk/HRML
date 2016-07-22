using System;
using System.Linq;

namespace HRLMParser
{
    internal static class QueryEvaluator
    {
        public static void Evaluate(string query, Tag parentTag)
        {
            var queryAttributeSplit = query.Split('~');
            var attributeName = queryAttributeSplit[1];
            var querySplitByDot = queryAttributeSplit[0].Split('.');

            var findTag = parentTag;

            var index = 0;

            foreach (var queryTagName in querySplitByDot)
            {
                if (index == 0)
                {
                    index++;
                    if (querySplitByDot.Length == index)
                    {
                        PrintValue(findTag, attributeName);
                    }
                    continue;
                }
                index++;

                findTag = FindTagInChildTags(findTag, queryTagName);

                if (findTag == null)
                {
                    Console.WriteLine("Not found!");
                    break;
                }

                if (querySplitByDot.Length != index) continue;

                PrintValue(findTag, attributeName);
            }
        }

        private static void PrintValue(Tag findTag, string attributeName)
        {
            if (findTag != null && findTag.TagAttribute != null
                && findTag.TagAttribute.Name == attributeName)
            {
                Console.WriteLine(findTag.TagAttribute.Value);
            }
            else
            {
                Console.WriteLine("Not found!");
            }
        }

        private static Tag FindTagInChildTags(Tag tag, string tagName)
        {
            return tag.Tags.FirstOrDefault(t => t.Name == tagName);
        }
    }
}