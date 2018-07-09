using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModule;
using System.Text.RegularExpressions;
using LogicModule.Interfaces;

namespace LogicModule.Models
{
    public class DataCollector
    {
        public int BattleType(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            const string pat = @"""battleType"":.(\d*),";
            Regex regex = new Regex(pat);
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0) return Convert.ToInt32(matches[0].Groups[1].Value);
            return -1;
        }

        public string DateTime(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            Regex regex = new Regex(@"""dateTime"":.""(\d*\.\d*\.\d* \d*:\d*:\d*)"",");
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0) return matches[0].Groups[1].Value;
            return null;
        }

        public string GetMapName(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            Regex regex = new Regex(@"""mapName"":\s""(\w*)"",");
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0) return matches[0].Groups[1].Value;
            return null;
        }

        public string GetPlayerName(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            Regex regex = new Regex(@"""playerName"":\s""(\w*)"",");
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0) return matches[0].Groups[1].Value;
            return null;
        }

        public string GetGamePlay(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            Regex regex = new Regex(@"""gameplayID"":\s""(\w*)"",");
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0) return matches[0].Groups[1].Value;
            return null;
        }

        public bool GetReplayStatus(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            string winner = null, playerVehicle = null, frags = null;
            Regex regex = new Regex(@"""winnerTeam"":\s(\d*),");
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0) winner = matches[0].Groups[1].Value;

            regex = new Regex(@"""playerVehicle"":\s(\w*)");
            matches = regex.Matches(text);
            if (matches.Count > 0) playerVehicle = matches[0].Groups[1].Value;

            regex = new Regex(@"""frags"":\s(\d*)");
            matches = regex.Matches(text);
            if (matches.Count > 0) frags = matches[0].Groups[1].Value;

            return (winner != null && playerVehicle != null && frags != null);
        }

        public bool HasMods(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            Regex regex = new Regex(@"""hasMods"":\s(\w*),");
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0) return Convert.ToBoolean(matches[0].Groups[1].Value);
            return false;
        }

        public int PlayerId(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            const string pat = @"""playerID"":.(\d*),";
            Regex regex = new Regex(pat);
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0) return Convert.ToInt32(matches[0].Groups[1].Value);
            return -1;
        }

        public List<Player> Players(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            Regex regex =
                new Regex(
                    @"""(\d*)"":\s{.*?""isTeamKiller"":\s(\w*),.*?""vehicleType"":\s""(\w*):(.*?)"",.*?""name"":\s""(\w*)"".*?""clanAbbrev"":\s""(.*?)"",.*?""team"":\s(\d),");
            MatchCollection matches = regex.Matches(text);
            List<Player> players = new List<Player>();
            if (matches.Count > 0)
            {
                foreach (Match math in matches)
                {
                    Player list = new Player();
                    if (math.Groups[1].Success) list.BattleId = Convert.ToInt32(math.Groups[1].Value);
                    if (math.Groups[2].Success) list.IsTeamKiller = Convert.ToBoolean(math.Groups[2].Value);
                    if (math.Groups[3].Success) list.VehicleNation = math.Groups[3].Value;
                    if (math.Groups[4].Success) list.VehicleName = math.Groups[4].Value;
                    if (math.Groups[5].Success) list.Name = math.Groups[5].Value;
                    if (math.Groups[6].Success) list.Clan = math.Groups[6].Value;
                    if (math.Groups[7].Success) list.Team = Convert.ToInt32(math.Groups[7].Value);
                    players.Add(list);
                }
                return players;
            }
            return null;
        }

        public Player PlayerStatistic(Player player, string text)
        {
            if (!GetReplayStatus(text)) return player;
            string par = "\"" + player.BattleId.ToString() + "\"";
            string par2 =
                @":\s\[\{(.*?)\}]";
            Regex reg = new Regex(par + par2);
            Match match = reg.Match(text);
             
            string tmpstring = "";
            if (match.Success) tmpstring = match.Value;

            #region Data
            reg = new Regex(@"""spotted"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.Spotted = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""damageAssistedTrack"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.DamageAssistedTrack = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""damageReceivedFromInvisibles"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.DamageReceivedFromInvisibles = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""directTeamHits"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.DirectTeamHits = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""damageDealt"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.DamageDealt = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""piercingsReceived"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.PiercingsReceived = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""sniperDamageDealt"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.SniperDamageDealt = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""damageAssistedRadio"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.DamageAssistedRadio = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""stunDuration"":\s(\d*\.\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.StunDuration = match.Groups[1].Value;
            reg = new Regex(@"""piercings"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.Piercings = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""damageBlockedByArmor"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.DamageBlockedByArmor = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""xp"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.Xp = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""droppedCapturePoints"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.DroppedCapturePoints = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""directHitsReceived"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.DirectHitsReceived = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""killerID"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.KillerId = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""deathReason"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.DeathReason = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""capturePoints"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.CapturePoints = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""health"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.Health = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""mileage"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.Mileage = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""shots"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.Shots = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""kills"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.Kills = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""damaged"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.Damaged = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""credits"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.Credits = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""accountDBID"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.AccountDbid = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""lifeTime"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.LifeTime = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""noDamageDirectHitsReceived"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.NoDamageDirectHitsReceived = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""potentialDamageReceived"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.PotentialDamageReceived = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""damageReceived"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.DamageReceived = Convert.ToInt32(match.Groups[1].Value);
            reg = new Regex(@"""directHits"":\s(\d*),");
            match = reg.Match(tmpstring);
            if (match.Success) player.DirectHits = Convert.ToInt32(match.Groups[1].Value);
            #endregion

            return player;
        }

        public string PlayerVehicle(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            Regex regex = new Regex(@"""playerVehicle"":\s""(.*?)""");
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0) return matches[0].Groups[1].Value;
            return null;
        }

        public string RegionCode(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            Regex regex = new Regex(@"""regionCode"":\s""(\w*)"",");
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0) return matches[0].Groups[1].Value;
            return null;
        }

        public string ServerName(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            Regex regex = new Regex(@"""serverName"":\s""(.*?)"",");
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0) return matches[0].Groups[1].Value;
            return null;
        }

        public int BattleDuration(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            Regex regex = new Regex(@"""duration"":\s(\d*),");
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0) return Convert.ToInt32(matches[0].Groups[1].Value);
            return -1;
        }

        public int Respawn(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            Regex regex = new Regex(@"""playerName"":\s""(\w*)"",");
            Match matches = regex.Match(text);
            string plname = "";
            if (matches.Success) plname = matches.Groups[1].Value;
            else return -1;

            string reg = $"\"vehicles\":\\s.*?\"name\":\\s\"{plname}\",.*?\"team\":\\s(\\d)";

            regex = new Regex(@reg);
            Match match = regex.Match(text);

            if (match.Success) return Convert.ToInt32(match.Groups[1].Value);
            return -1;
        }

        public int Winner(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            Regex regex = new Regex(@"""winnerTeam"":\s(\d*),");
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0) return Convert.ToInt32(matches[0].Groups[1].Value);
            return -1;
        }

        public string Version(string text)
        {
            if (text == null) throw new ArgumentNullException(text);
            Regex regex = new Regex(@"""clientVersionFromXml"":\s""(.*?)"",");
            Match matches = regex.Match(text);
            if (matches.Success) return matches.Groups[1].Value;
            return null;
        }

    }
}
