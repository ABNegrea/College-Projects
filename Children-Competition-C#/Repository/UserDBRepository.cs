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
    internal class UserDBRepository : IUserRepository
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserDBRepository));
        private readonly string connectionString;

        public UserDBRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public User Delete(Guid gid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string id = gid.ToString();

                using (var command = new SQLiteCommand("DELETE FROM User WHERE id = @id", connection))
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

        public IEnumerable<User> FindAll()
        {
            var users = new List<User>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM User", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetGuid(0);
                        var firstName = reader.GetString(1);
                        var lastName = reader.GetString(2);
                        var email = reader.GetString(3);
                        var password = reader.GetString(4);
                        var office = reader.GetInt32(5);
                        var user = new User(id,firstName,lastName,email,password, office);
                        users.Add(user);
                    }
                }
            }
            log.Info($"Found {users.Count} items in the repository.");
            return users;
        }

        public User FindByEmailPassword(string email, string password)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SQLiteCommand("SELECT * FROM User WHERE password = @email" +
                    " AND password = @password", connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var id = reader.GetGuid(reader.GetOrdinal("id"));
                            var firstName = reader.GetString(reader.GetOrdinal("firstName"));
                            var lastName = reader.GetString(reader.GetOrdinal("lastName"));
                            var office = reader.GetInt32(reader.GetOrdinal("office"));
                            log.Info($"FindOne item {reader.GetGuid(0)} to the repository.");
                            return new User(id, firstName, lastName, email, password, office);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public User FindOne(Guid gid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string id = gid.ToString();

                using (var command = new SQLiteCommand("SELECT * FROM User WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var firstName = reader.GetString(reader.GetOrdinal("firstName"));
                            var lastName = reader.GetString(reader.GetOrdinal("lastName"));
                            var email = reader.GetString(reader.GetOrdinal("email"));
                            var password = reader.GetString(reader.GetOrdinal("password"));
                            var office = reader.GetInt32(reader.GetOrdinal("office"));
                            log.Info($"FindOne item {reader.GetGuid(0)} to the repository.");
                            return new User(gid,firstName,lastName,email,password,office);
                        }
                        else
                        {
                            return null;
                        }   
                    }
                }
            }
        }

        public User Save(User entity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("INSERT INTO User (id, firstName, lastName, email, password, office)" +
                    " VALUES (@id, @firstName, @lastName, @email, @password, @office);", connection))
                {
                    command.Parameters.AddWithValue("@firstName", entity.FirstName);
                    command.Parameters.AddWithValue("@lastName", entity.LastName);
                    command.Parameters.AddWithValue("@email", entity.Email);
                    command.Parameters.AddWithValue("@password", entity.Password);
                    command.Parameters.AddWithValue("@office", entity.Office);
                    command.Parameters.AddWithValue("@id", entity.Id);
                    log.Info($"Added item {entity.Id} to the repository.");
                }
            }
            return entity;
        }

        public User Update(Guid gid, User entity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string id = gid.ToString();

                using (var command = new SQLiteCommand("UPDATE User SET firstName = @firstName, lastName = @lastName, " +
                    " email = @email, password = @password, office=@office WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@firstName", entity.FirstName);
                    command.Parameters.AddWithValue("@lastName", entity.LastName);
                    command.Parameters.AddWithValue("@email", entity.Email);
                    command.Parameters.AddWithValue("@password", entity.Password);
                    command.Parameters.AddWithValue("@office", entity.Office);
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
