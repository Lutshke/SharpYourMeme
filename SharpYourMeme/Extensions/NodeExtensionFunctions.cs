using HtmlAgilityPack;

namespace SharpYourMeme.Extensions
{
    public static class NodeExtensionFunctions
    {
        /// <summary>
        /// Returns all child nodes which are NodeType Element
        /// </summary>
        /// <param name="articleNode"></param>
        /// <returns></returns>
        internal static List<HtmlNode> GetElementNodes(this HtmlNode articleNode)
        {
            List<HtmlNode> nodes = new();
            for (int childNodes = 0; childNodes < articleNode.ChildNodes.Count; childNodes++)
            {
                if (articleNode.ChildNodes[childNodes].NodeType == HtmlNodeType.Element)
                {
                    nodes.Add(articleNode.ChildNodes[childNodes]);
                }
            }
            return nodes;
        }

        /// <summary>
        /// This returns the first element node. If none is found it return the original node
        /// </summary>
        /// <param name="articleNode"></param>
        /// <returns></returns>
        internal static HtmlNode GetFirstElementNode(this HtmlNode articleNode)
        {
            var childNodes = articleNode.ChildNodes;
            for (int index = 0; index < childNodes.Count; index++)
            {
                if (childNodes[index].NodeType == HtmlNodeType.Element)
                {
                    return childNodes[index];
                }
            }

            return null;
        }
    }
}
