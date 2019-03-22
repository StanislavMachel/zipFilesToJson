using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ControlPanel.Web.Entities
{
    public class TreeItem
    {
        public TreeItem(string entryName, string name, string type)
        {
            EntryName = entryName;
            Name = name;
            Type = type;
        }

        public string Type { get; set; }

        public string Name { get; set; }
        [JsonIgnore] public string EntryName { get; set; }

        public List<TreeItem> Children { get; } = new List<TreeItem>();

        public void AddChildren(TreeItem treeItem)
        {
            Children.Add(treeItem);
        }

        public bool ShouldSerializeChildren()
        {
            return Children.Count > 0;
        }
    }
}
