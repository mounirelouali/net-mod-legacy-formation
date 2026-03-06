using System.Collections.Generic;

namespace generationxml
{
    public class TagRule
    {
        public string TagName { get; set; }
        public List<IRule> Rules { get; set; } = new List<IRule>();
    }
}