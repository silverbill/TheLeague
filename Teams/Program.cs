using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Teams
{
    class Program
    {
        static void Main(string[] args)
        {
            var football = new Sport(sportType.Football);
            IEnumerable<League> leagues = "Eastern, Western".Split(new char[] { ',' }).Select(s => new League(s.Trim()));
            IEnumerable<Team> teamsEast = "Bills, Cowboys, Raiders".Split(new char[] { ',' }).Select(s => new Team(s.Trim()));
            IEnumerable<Team> teamsWest = "Broncos, Niners, Giants".Split(new char[] { ',' }).Select(s => new Team(s.Trim()));

            IEnumerable<Coach> coaches6 = "Belichik, Parcells, Garrett, Shula, Landry, Ryan".Split(new char[] { ',' }).Select(s => new Coach(s.Trim()));

            IEnumerable<Player> playersCowboys = "Witten, Romo, Newton".Split(new char[] { ',' }).Select(s => new Player(s.Trim()));
            IEnumerable<Player> playersBills = "3time, OchoCinco, Kelly".Split(new char[] { ',' }).Select(s => new Player(s.Trim()));
            IEnumerable<Player> playersRaiders = "Jones, Turn, Who'sonfirst".Split(new char[] { ',' }).Select(s => new Player(s.Trim()));
            IEnumerable<Player> playersBroncos = "Manning, Elway, Atwater".Split(new char[] { ',' }).Select(s => new Player(s.Trim()));
            IEnumerable<Player> playersNiners = "Montana, Rice, Rathman".Split(new char[] { ',' }).Select(s => new Player(s.Trim()));
            IEnumerable<Player> playersGiants = "Manning, Taylor, Payton".Split(new char[] { ',' }).Select(s => new Player(s.Trim()));

            //let the concat'ing begin
            var leaguesToSport = football;
            football.leaguesMain = football.leaguesMain.Concat(leagues); //leagues into the sport
            var Eastern = leagues.ElementAt(0);
            Eastern.teams = Eastern.teams.Concat(teamsEast);  //teams into conferences
            var Western = leagues.ElementAt(1);
            Western.teams = Western.teams.Concat(teamsWest);

            var Cowboys = new Team("Cowboys");
            Cowboys = teamsEast.ElementAt(1);
            Cowboys.coaches = Cowboys.coaches.Concat(coaches6.Where(c => c.name == "Parcells"));  //coaches into teams
                                                                                                  //where cowboys is                                           //where coach is
            var Bills = new Team("Bills");
            Bills = teamsEast.ElementAt(0);
            Bills.coaches = Bills.coaches.Concat(coaches6.Where(c => c.name == "Garrett"));
            var Raiders = new Team("Raiders");
            Raiders = teamsEast.ElementAt(2);
            Raiders.coaches = Raiders.coaches.Concat(coaches6.Where(c => c.name == "Shula"));
            var Broncos = new Team("Broncos");
            Broncos = teamsWest.ElementAt(0);
            Broncos.coaches = Broncos.coaches.Concat(coaches6.Where(c => c.name == "Landry"));
            var Niners = new Team("Niners");
            Niners = teamsEast.ElementAt(1);
            Niners.coaches = Niners.coaches.Concat(coaches6.Where(c => c.name == "Ryan"));
            var Giants = new Team("Giants");
            Giants = teamsEast.ElementAt(2);
            Giants.coaches = Giants.coaches.Concat(coaches6.Where(c => c.name == "Belichik"));

            Cowboys.players = Cowboys.players.Concat(playersCowboys);
            Giants.players = Giants.players.Concat(playersGiants);
            Raiders.players = Raiders.players.Concat(playersRaiders);
            Bills.players = Bills.players.Concat(playersBills);
            Niners.players = Niners.players.Concat(playersNiners);
            Broncos.players = Broncos.players.Concat(playersBroncos);



            Directory.CreateDirectory("html");
            File.WriteAllText(@"html/index.html", football.ToString());

            Console.WriteLine(Cowboys);
            Console.ReadLine();
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
            public sportType sport;
            public IEnumerable<League> leaguesMain = new List<League>();
            public IEnumerable<Team> allTeams = new List<Team>(); //- should return all the Teams in the sport
            public IEnumerable<Player> playerMostPoints = new List<Player>(); //- should return the Player in the sport with the most points
            public IEnumerable<Coach> topCoach = new List<Coach>(); //- should return the Coach whose Team's Players had the highest average points
            public Sport(sportType s)
            {
                sport = s;
            }
            public override string ToString()
            {
                string b = String.Join(", ", leaguesMain);
                string r = String.Join(", ", allTeams);
                string p = String.Join(", ", playerMostPoints);
                string t = String.Join(", ", topCoach);
                return String.Format
                    (@"
                    <div>
                        <h1>{0}</h1>
                        <p>Leagues: {1}</p>
                        <p>Teams: {2}</p>
                        <p>Top Scorer: {3}</p>
                        <p>Top Coach: {4}</p>
                    </div>", sport, b, r, p, t);
            }
        }
        class League
        {
            public IEnumerable<Team> teams = new List<Team>();
            public IEnumerable<Team> allTeams = new List<Team>();
            public string name;
            public League(string name)
            {
                this.name = name;
            }
            public override string ToString()
            {
                string r = String.Join(", ", teams);
                return String.Format("{0} : {1}", name, r);
            }
        }
        class Team
        {
            public string name;
            public string hometown;
            public IEnumerable<Coach> coaches = new List<Coach>();
            //public IEnumerable<Coach> coaches6 = new List<Coach>();
            public IEnumerable<Player> players = new List<Player>();

            public Team(string name)
            {
                this.name = name;
            }

            public override string ToString()
            {
                string r = String.Join(", ", coaches);
                string p = String.Join(", ", players);
                return String.Format("{0} || Coach : {1}|| Players : {2}", name, r, p);
            }
        }
        class Coach
        {
            public string name;
            public Coach(string name)
            {
                this.name = name;
            }
            public override string ToString()
            {
                return $"{name}";
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
            public override string ToString()
            {
                return $"{name}";
            }
        }
        class Score
        {
            internal int score { get; private set; }
        }
    }
}