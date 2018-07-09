using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModule;

namespace LogicModule.Interfaces
{
    interface IDataCollector
    {
        string GetPath();
        string GetPlayerName(string text);
        string GetMapName(string text);
        bool GetReplayStatus(string text);
        string GetFileName();
        int GetFileSize();
        bool HasMods(string text);
        string RegionCode(string text);
        int PlayerId(string text);
        string ServerName(string text);
        Player PlayerStatistic(Player player, string text);
        List<Player> Players(string text);
        string DateTime(string text);
        int BattleType(string text);
        string PlayerVehicle(string text);

    }
}
