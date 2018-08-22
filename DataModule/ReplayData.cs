using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModule
{
    public class ReplayData
    {
        public string Path { get; set; }
        public string FullPath { get; set; }
        public string Name { get;  set; }
        public string Map { get;  set; }
        public bool Status { get;  set; }
        public string GamePlayId { get; set; }
        public int Size { get;  set; }
        public string Player { get;  set; }
        public string Vehicle { get;  set; }
        public string Version { get;  set; }
        public string DateTime { get;  set; }
        public int Duration { get;  set; }
        public int Respawn { get;  set; }
        public int Winner { get;  set; }
        public string RegionCode { get; set; }
        public int BattleType { get; set; }
        public bool HasMods { get; set; }
        public long PlayerId { get; set; } 
        public string ServerName { get; set; }
        public List<Player> Players { get; set; }

    }
}
