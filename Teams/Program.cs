using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teams
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        public enum sportType
            {
                Tennis,
                Football,
                Basketball,
                Boxing,
                Baseball
            }

        class Sport
        {
            public IEnumerable<League> leagues;
            public sportType sport;
            public IEnumerable<Team> allTeams = new List<Team>(); //- should return all the Teams in the sport
            public IEnumerable<Player> playerMostPoints = new List<Player>(); //- should return the Player in the sport with the most points
            public IEnumerable<Coach> topCoach = new List<Coach>(); //- should return the Coach whose Team's Players had the highest average points
                public Sport(sportType sport)
                {
                    this.sport = sport;
                }
            }
        class League
        {
            public IEnumerable<Team> teams = new List<Team>();
            public string name;
            public League(string name)
            {
                this.name = name;
            }
        }
        class Team
        {
            public IEnumerable<Coach> coaches = new List<Coach>();
            public IEnumerable<Player> players = new List<Player>();
            public string name;
            public string hometown;
                public Team(string name)
            {
                this.name = name;
            }
        }
        class Coach
        {
            public string name;
            public Coach(string name)
            {
                this.name = name;
            }
        }
        class Player
        {
            public string name;
            public Score points;
            public Player(string name)
            {
                this.name = name;
            }

        }
        class Score
        {
            internal int score { get; private set; }
        }
    }
}
