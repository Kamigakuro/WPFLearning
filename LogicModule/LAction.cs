using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DataModule;
using LogicModule.Interfaces;
using LogicModule.Models;

namespace LogicModule
{
    public class LAction
    {
        public FileInfo[] ScanFolder(string path, bool allDirectories)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            var replayfiles = dir.GetFiles("*.wotreplay", allDirectories ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly);

            return replayfiles;
        }

        public List<ReplayData> ScanReplays(FileInfo[] files, int lines = 0)
        {
            if (files == null || files.Length <= 0) throw new Exception("Param 'files' null or empty");

            List<ReplayData> replays = new List<ReplayData>();
            foreach (var file in files) replays.Add(SingleScan(file, lines));
            return replays;
        }

        public ReplayData SingleScan(FileInfo file, int lines)
        {
            if (file != null)
            {
                if (file.Exists)
                {
                    ReplayData replay = new ReplayData();
                    DataCollector collector = new DataCollector();

                    replay.Path = file.DirectoryName;
                    replay.Name = file.Name;
                    replay.FullPath = file.FullName;
                    replay.Size = Convert.ToInt32(file.Length / 1024);
                    string text = String.Empty;
                    if (lines > 0) ReplayReader.ReadReplay(replay.FullPath, lines);
                    else text = ReplayReader.ReadReplay(replay.FullPath);

                    if (String.IsNullOrEmpty(text)) return replay;
                    bool isValidReplay = collector.GetReplayStatus(text);

                    replay.DateTime = collector.DateTime(text);
                    replay.Map = collector.GetMapName(text);
                    replay.Player = collector.GetPlayerName(text);
                    replay.GamePlayId = collector.GetGamePlay(text);
                    replay.Respawn = collector.Respawn(text);
                    replay.Players = collector.Players(text);
                    replay.Status = isValidReplay;
                    replay.Vehicle = collector.PlayerVehicle(text);
                    replay.Version = collector.Version(text);
                    replay.BattleType = collector.BattleType(text);
                    replay.HasMods = collector.HasMods(text);
                    replay.RegionCode = collector.RegionCode(text);
                    replay.ServerName = collector.ServerName(text);
                    replay.PlayerId = collector.PlayerId(text);

                    if (isValidReplay)
                    {
                        replay.Duration = collector.BattleDuration(text);
                        replay.Winner = collector.Winner(text);
                        replay.Status = collector.GetReplayStatus(text);
                        
                    }

                    return replay;

                }
            }

            return null;

        }

    }
}
