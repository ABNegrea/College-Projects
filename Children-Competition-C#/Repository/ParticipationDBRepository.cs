using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concurs.Domain;
using Concurs.Repository.Interfaces;
using log4net;
using System.Data.SQLite;

namespace Concurs.Repository
{
    internal class ParticipationDBRepository : IParticipationRepository
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserDBRepository));
        private readonly string connectionString;
        private ChildDBRepository childRepo;
        private EventDBRepository eventRepo;

        public ParticipationDBRepository(string connectionString, ChildDBRepository childRepo, EventDBRepository eventRepo)
        {
            this.connectionString = connectionString;
            this.eventRepo = eventRepo;
            this.childRepo = childRepo;
        }
        public Participation Delete(Guid gid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string id = gid.ToString();

                using (var command = new SQLiteCommand("DELETE FROM Participation WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return null;
                    }
                }
                log.Info($"Remove item {gid} from the repository.");
                return null;
            }
        }

        public IEnumerable<Participation> FindAll()
        {
            var particips = new List<Participation>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Participation", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetGuid(0);
                        var idChild = reader.GetGuid(1);
                        var idEvent = reader.GetGuid(2);
                        var part = new Participation(id, childRepo.FindOne(idChild), eventRepo.FindOne(idEvent));  
                        particips.Add(part);
                    }
                }
            }
            log.Info($"Found {particips.Count} items in the repository.");
            return particips;
        }

        public Participation FindOne(Guid gid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string id = gid.ToString();

                using (var command = new SQLiteCommand("SELECT * FROM Participation WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var idChild = reader.GetString(reader.GetOrdinal("idChild"));
                            var idEvent = reader.GetString(reader.GetOrdinal("idEvent"));

                            log.Info($"FindOne item {reader.GetGuid(0)} to the repository.");
                            return new Participation(gid, childRepo.FindOne(Guid.Parse(idChild)),
                                eventRepo.FindOne(Guid.Parse(idEvent)));
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public int ParticipationCountChild(Guid gid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                int cnt = 0;
                string idChild = gid.ToString();

                using (var command = new SQLiteCommand("SELECT * FROM Participation WHERE idChild = @idChild", connection))
                {
                    command.Parameters.AddWithValue("@idChild", idChild);

                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                            cnt++;
                    }
                    int test = command.ExecuteNonQuery();
                    if (test == 0)
                        return 0;
                    log.Info($"ParticipationCountChild fount {cnt} children repository.");
                    return cnt;
                }
            }
        }

        public Participation Save(Participation entity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("INSERT INTO Participation (id, idChild, idEvent)" +
                    " VALUES (@id, @idChild, @idEvent);", connection))
                {
                    command.Parameters.AddWithValue("@idChild", entity.Child.Id.ToString());
                    command.Parameters.AddWithValue("@idEvent", entity.Event.Id.ToString());
                    command.Parameters.AddWithValue("@id", entity.Id.ToString());
                    int test = command.ExecuteNonQuery();
                    if (test == 0)
                        return null;
                    log.Info($"Added item {entity.Id} to the repository.");
                }
            }
            return entity;
        }

        public Participation Update(Guid gid, Participation entity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string id = gid.ToString();

                using (var command = new SQLiteCommand("UPDATE Participation SET idChild = @idChild, idEvent = @idEvent, " +
                    "WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@idChild", entity.Child.Id.ToString());
                    command.Parameters.AddWithValue("@idEvent", entity.Event.Id.ToString());
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return null;
                    }
                }
                log.Info($"Update item {id} from in repository.");
                entity.Id = gid;
                return entity;
            }
        }
    }
}
