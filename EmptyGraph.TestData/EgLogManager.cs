using System;
using System.Collections.Generic;
using System.IO;
using EmptyGraph.TestData.Properties;
using Newtonsoft.Json;

namespace EmptyGraph.Log
{
    public class EgLogManager
    {
        public int TestRunId { get; set; }
        public List<DataEntry> DataEntries { get; set; } = new List<DataEntry>();

        public void AddEntry(string id, string type, string content)
        {
            var entry = new DataEntry()
            {
                Id = id,
                Type = type,
                Content = content
            };

            if (! DataEntries.Contains(entry))
            {
                DataEntries.Add(entry);
            }
        }

        public void Save()
        {
            var logPath = String.Concat(Settings.Default.LogPath, this.TestRunId, ".json");
            File.WriteAllText(logPath, JsonConvert.SerializeObject(this));
        }
    }
}
