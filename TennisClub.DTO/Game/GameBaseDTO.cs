using System;
using System.Collections.Generic;
using System.Text;

namespace TennisClub.DTO.Game
{
    public class GameBaseDTO
    {
        public string GameNumber { get; set; }
        public int MemberId { get; set; }
        public byte LeagueId { get; set; }
        public DateTime Date { get; set; }
    }
}
