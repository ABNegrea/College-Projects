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
    internal class ChildDBRepository: IChildRepository
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserDBRepository));
        private readonly string connectionString;

        public ChildDBRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Child Delete(Guid gid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string id = gid.ToString();

                using (var command = new SQLiteCommand("DELETE FROM Child WHERE id = @id", connection))
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

        public IEnumerable<Child> FindAll()
        {
            var children = new List<Child>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Child", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetGuid(0);
                        var firstName = reader.GetString(1);
                        var lastName = reader.GetString(2);
                        var age = reader.GetInt32(3);
                        var chd = new Child(id,firstName,lastName,age);
                        children.Add(chd);
                    }
                }
            }
            log.Info($"Found {children.Count} items in the repository.");
            return children;
        }

        public Child FindChildByNameAge(string firstName, string lastName, int age)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SQLiteCommand("SELECT * FROM Child WHERE firstName = @firstName and " +
                    "lastName = @lastName and age = @age", connection))
                {
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@age", age);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string id = reader.GetString(reader.GetOrdinal("id"));

                            log.Info($"FindOne item {id} to the repository.");
                            return new Child(Guid.Parse(id), firstName, lastName, age);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public Child FindOne(Guid gid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string id = gid.ToString();

                using (var command = new SQLiteCommand("SELECT * FROM Child WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var firstName = reader.GetString(reader.GetOrdinal("firstName"));
                            var lastName = reader.GetString(reader.GetOrdinal("lastName"));
                            var age = reader.GetInt32(reader.GetOrdinal("age"));

                            log.Info($"FindOne item {reader.GetGuid(0)} to the repository.");
                            return new Child(gid, firstName, lastName, age);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public Child Save(Child entity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("INSERT INTO Child (id, firstName, lastName, age)" +
                    " VALUES (@id, @firstName, @lastName, @age);", connection))
                {
                    command.Parameters.AddWithValue("@firstName", entity.FirstName);
                    command.Parameters.AddWithValue("@lastName", entity.LastName);
                    command.Parameters.AddWithValue("@age", entity.Age);
                    command.Parameters.AddWithValue("@id", entity.Id.ToString());
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return null;
                    }
                    log.Info($"Added item {entity.Id} to the repository.");
                }
                
            }
            return entity;
        }

        public Child Update(Guid gid, Child entity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string id = gid.ToString();

                using (var command = new SQLiteCommand("UPDATE Child SET firstName = @firstName, lastName = @lastName, " +
                    " age = @age WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@firstName", entity.FirstName);
                    command.Parameters.AddWithValue("@lastName", entity.LastName);
                    command.Parameters.AddWithValue("@age", entity.Age);
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
