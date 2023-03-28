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
    internal class EventDBRepository : IEventRepository
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserDBRepository));
        private readonly string connectionString;

        public EventDBRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int AddEnrolledToEvent(Guid id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                Event ev = this.FindOne(id);
                var enrolled = ev.Enrolled + 1;

                using (var command = new SQLiteCommand("UPDATE Event SET enrolled=@enrolled WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@enrolled", enrolled);
                    command.Parameters.AddWithValue("@id", id.ToString());

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return 0;
                    }
                }
                log.Info($"Update ite@@@@@@@@@@@@@@@@@@@@@@@@@@@m {enrolled} from repository.");
                return enrolled;
            }
        }

        public Event Delete(Guid gid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string id = gid.ToString();

                using (var command = new SQLiteCommand("DELETE FROM Event WHERE id = @id", connection))
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

        public IEnumerable<Event> FindAll()
        {
            var events = new List<Event>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Event", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetGuid(0);
                        var name = reader.GetString(1);
                        var minAge = reader.GetInt32(2);
                        var maxAge = reader.GetInt32(3);
                        var enrolled = reader.GetInt32(4);
                        var evn = new Event(id, name, minAge, maxAge, enrolled);
                        events.Add(evn);
                    }
                }
            }
            log.Info($"Found {events.Count} items in the repository.");
            return events;
        }

        public Event FindByNameAge(string name, int ageInRange)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
;

                using (var command = new SQLiteCommand("SELECT * FROM Event WHERE name = @name and minAge<=@ageInRange " +
                    "and maxAge>=@ageInRange2", connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@ageInRange",ageInRange);
                    command.Parameters.AddWithValue("@ageInRange2", ageInRange);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var gid = Guid.Parse(reader.GetString(reader.GetOrdinal("id")));
                            var namerez = reader.GetString(reader.GetOrdinal("name"));
                            var minAge = reader.GetInt32(reader.GetOrdinal("minAge"));
                            var maxAge = reader.GetInt32(reader.GetOrdinal("maxAge"));
                            var enrolled = reader.GetInt32(reader.GetOrdinal("enrolled"));

                            log.Info($"FindOne item {reader.GetGuid(0)} to the repository.");
                            return new Event(gid, namerez, minAge, maxAge, enrolled);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public Event FindOne(Guid gid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string id = gid.ToString();

                using (var command = new SQLiteCommand("SELECT * FROM Event WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var name = reader.GetString(reader.GetOrdinal("name"));
                            var minAge = reader.GetInt32(reader.GetOrdinal("minAge"));
                            var maxAge = reader.GetInt32(reader.GetOrdinal("maxAge"));
                            var enrolled = reader.GetInt32(reader.GetOrdinal("enrolled"));

                            log.Info($"FindOne item {reader.GetGuid(0)} to the repository.");
                            return new Event(gid, name, minAge, maxAge,enrolled);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public Event Save(Event entity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("INSERT INTO Event (id, name, minAge, maxAge, enrolled)" +
                    " VALUES (@id, @name, @minAge, @maxAge, @enrolled);", connection))
                {
                    command.Parameters.AddWithValue("@name", entity.Name);
                    command.Parameters.AddWithValue("@minAge", entity.MinAge);
                    command.Parameters.AddWithValue("@maxAge", entity.MaxAge);
                    command.Parameters.AddWithValue("@id", entity.Id);
                    command.Parameters.AddWithValue("@enrolled", entity.Enrolled);
                    log.Info($"Added item {entity.Id} to the repository.");
                }
            }
            return entity;
        }

        public Event Update(Guid gid, Event entity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string id = gid.ToString();

                using (var command = new SQLiteCommand("UPDATE Event SET name = @name, minAge = @minAge," +
                    " maxAge = @maxAge WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@name", entity.Name);
                    command.Parameters.AddWithValue("@minAge", entity.MinAge);
                    command.Parameters.AddWithValue("@maxAge", entity.MaxAge);
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
