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

            Sport football = new Sport(sportType.Football);
            
            League NFL = new League("NFL");
            football.league = NFL;
                                 
            Team cowboys = new Team("Cowboys");
            cowboys.addRosterFromCSVString("Witten-22, Romo-42, Newton-42");
            cowboys.addCoachesFromString("Parcells,Landry,Johnson");
            NFL.addTeam(cowboys);
            

            Team giants = new Team("Giants");
            giants.addRosterFromCSVString("Manning-12, Taylor-13, Payton-14");
            giants.addCoachesFromString("Parcells");
            NFL.addTeam(giants); 

            Team niners = new Team("Niners");
            niners.addRosterFromCSVString("Montana-567, Rice-54, Rathman-65");
            niners.addCoachesFromString("Siefert");
            NFL.addTeam(niners); 

            Team bills = new Team("Bills");
            bills.addRosterFromCSVString("3time-23, OchoCinco-76, Kelly-12");
            bills.addCoachesFromString("Johnson, Smith");
            NFL.addTeam(bills);

            IEnumerable<Player> playersList = NFL.getAllPlayers();
                foreach (Player player in playersList)
            {
                Console.WriteLine(player.ToString());
            }
            int totalLeagueScore = NFL.getTotalLeagueScore();
            Console.WriteLine(totalLeagueScore);

            IEnumerable<Team> teamList = NFL.getAllTeams();
                foreach(Team team in teamList)
            {
                Console.WriteLine(team.name + " " + team.getTeamTotals());
            }
            Player bestPlayer = NFL.getBestPlayer(); //sumtingwong.need better way to get highest points
            Console.WriteLine(bestPlayer.ToString());

            Team bestTeam = NFL.topCoach();
            Console.WriteLine(teamList.ToString());
            Console.ReadLine();

            Console.WriteLine(football);
            Console.ReadLine();
            Directory.CreateDirectory("html");
            File.WriteAllText(@"html/index.html", NFL.ToString());

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
            public sportType sporttype;
            public League league;         
            public IEnumerable<Player> playerMostPoints = new List<Player>(); //- should return the Player in the sport with the most points
            public IEnumerable<Coach> topCoach = new List<Coach>(); //- should return the Coach whose Team's Players had the highest average points
            public Sport(sportType s)
            {
                sporttype = s;
            }
            public void addTeam(Team name)
            {
                league.teams.Add(name);
            }
           
            public override string ToString()
            {
                //string b = String.Join(", ", league);
                string x = String.Join(", ", league.teams);
                string p = String.Join(", ", playerMostPoints);
                string t = String.Join(", ", topCoach);
                return String.Format
                    (@"
                    <div>
                        <h1>{0}</h1>
                        <p>League: NFL</p>
                        <p>Teams: {1}</p>
                        <p>Top Scorer: {2}</p>
                        <p>Top Coach: {3}</p>
                    </div>", sporttype, x, p, t);
             }

        }
        class League
        {
            public List<Team> teams = new List<Team>();
            public List<Coach> coaches = new List<Coach>();
            public string name;            
            public League(string name)
            {
                this.name = name;
            }
            public void addTeam(Team name)
            {
                teams.Add(name);
            }
            public void addTeamsFromCSV(String names)
            {
                String[] tempTeams = names.Split(',');
                for(int i=0; i < tempTeams.Length; i++)
                {
                    String teamName = tempTeams[i];
                    Team tempTeam = new Team(teamName);
                    teams.Add(tempTeam);
                }
            }
            public Player getBestPlayer()
            {
                List<Player> sortedList = new List<Player>();
                Player currentPlayer = null;
                Player newPlayer = null;
                
                foreach (Team team in teams)
                {
                    foreach (Player player in team.players)
                    {
                        sortedList.Add(player);
                    }
                }
                currentPlayer = sortedList.ElementAt(0);    //compareTo
                newPlayer = sortedList.ElementAt(1);
                for(int i=0; i < sortedList.Count; i++)
                {
                    currentPlayer = sortedList.ElementAt(i);
                    newPlayer = sortedList.ElementAt(i++);
                    if (currentPlayer.score > newPlayer.score)
                    {
                        currentPlayer = sortedList.ElementAt(i);
                    }
                    else
                    {
                        currentPlayer = sortedList.ElementAt(i++);
                    }
                }
                return currentPlayer;
                
            }
            public Team topCoach()
            {
                List<Team> sortedList = new List<Team>();
                Team currentTeam = null;
                Team newTeam = null;

                foreach (Team team in teams)
                {
                        sortedList.Add(team);                  
                }
                currentTeam = sortedList.ElementAt(0);    //compareTo
                newTeam = sortedList.ElementAt(1);
                for (int i = 0; i < sortedList.Count; i++)
                {
                    currentTeam = sortedList.ElementAt(i);
                    newTeam = sortedList.ElementAt(i++);
                    if (currentTeam.getTeamTotals() > newTeam.getTeamTotals())
                    {
                        currentTeam = sortedList.ElementAt(i);
                    }
                    else
                    {
                        currentTeam = sortedList.ElementAt(i++);  //***exception at runtime and not sure why this doesn't work.
                    }
                }
                return currentTeam;

            }

            public IEnumerable <Player> getAllPlayers()
            {
                List<Player> tempList = new List<Player>();
                foreach (Team team in teams)
                {
                    foreach (Player player in team.players)
                    {
                        tempList.Add(player);
                    }
                }
                return tempList;
            }
            public int getTotalLeagueScore()
            {
                int tempScore = 0;
                foreach (Team team in teams)
                {
                    foreach (Player player in team.players)
                    {
                        tempScore = tempScore + player.score;
                    }
                }
                return tempScore;
            }
            public IEnumerable<Team> getAllTeams()
            {
                return teams;
            }
            
            public override string ToString()
            {
                string r = String.Join(", ", teams);
                string x = String.Join(" ,", coaches);
                return String.Format("{0} : {1} : {2}",name, r, x);
            }
        }
        //class Conferences
        //{
        //    public string name;
        //    public List<Team> teams;
        //    public Conferences(string name)
        //    {
        //        this.name = name;
        //    }
        //}
        class Team
        {
            public string name;
            public string hometown;
            public List<Coach> coaches = new List<Coach>();
            public List<Player> players = new List<Player>();
            public List<Score> scores = new List<Score>();  //added to test if teams.player[0].points would print points.  It doesn't.

            public Team(string name)
            {
                this.name = name;
            }
            public int getTeamTotals()
            {
                int tempTeamTotal = 0;
                foreach (Player player in players)
                {
                    tempTeamTotal = tempTeamTotal + player.score;
                }
                return tempTeamTotal;
            }
            public void addRosterFromCSVString (String names)  
            {
                String[] tempArray = names.Split(',');
                for (int i = 0; i < tempArray.Length; i++)
                {
                    String name = tempArray[i];
                    String tempName = "";
                    int tempScore = 0;
                    String[] tempT = name.Split('-');
                    tempName = tempT[0];
                    tempScore = Int32.Parse(tempT[1]);  //takes the score and casts it into its integer equivalent
                    Player tempPlayer = new Player(tempName,tempScore);
                    players.Add(tempPlayer);
                    
                }
            }
         
            public void addRosterFromString(String names)
            {
                IEnumerable <Player> tempPlayers = new String(names.ToCharArray()).Split(new char[] { ',' }).Select(s => new Player(s.Trim()));
                players.AddRange(tempPlayers);
            }
            public void addCoachesFromString(String names)
            {
                {
                    IEnumerable<Coach> tempCoach = new String(names.ToCharArray()).Split(new char[] { ',' }).Select(s => new Coach(s.Trim()));
                    coaches.AddRange(tempCoach);
                }
            }

            public override string ToString()
            {
                string r = String.Join(", ", coaches);
                string y = String.Join(", ", players);
                return String.Format("Team: {0} || Coaches: {1} Players: {2}", name, r, y);
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
                return name;
            }
        }
        class Player
        {
            public string name;
            public int score;
            public List<Player> players = new List<Player>();
            public Player (string name)
            {
                this.name = name;
                this.score = 0;
            }

            public Player(string name, int score)
            {
                this.name = name;
                this.score = score;
            }
            public void addScoreTo(int x)
            {
                this.score = this.score + x;
            }

            public override string ToString()
            {
                string r = String.Join(", ", score);
                return String.Format("Name: {0} || points: {1}", name, r);
            }
        }
        class Score
        {
            public int points;
            public Score (int pointTotal)
            {
                points = pointTotal;
            }
        }
    }   
}
