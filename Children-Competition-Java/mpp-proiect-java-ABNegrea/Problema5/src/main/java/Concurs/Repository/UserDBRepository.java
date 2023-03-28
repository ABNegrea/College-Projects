package Concurs.Repository;

import Concurs.Domain.User;
import Concurs.Repository.Interfaces.JdbcUtils;
import Concurs.Repository.Interfaces.UserRepository;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Properties;
import java.util.UUID;


public class UserDBRepository implements UserRepository {
    private JdbcUtils dbUtils;
    private static final Logger logger = LogManager.getLogger();

    public UserDBRepository(Properties props) {
        logger.info("Initializing UserDBRepository with properties: {} ", props);
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public User findByID(UUID uuid) throws IllegalArgumentException {
        logger.traceEntry("find by id task {}");
        Connection con = dbUtils.getConnection();
        User rez = null;
        try (PreparedStatement preparedStatement = con.prepareStatement("select * from User where id = ?")) {
            preparedStatement.setObject(1, uuid);
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    UUID id = UUID.fromString(resultSet.getString("id"));
                    //UUID id = (UUID) resultSet.getString("id");
                    String firstName = resultSet.getString("firstName");
                    String lastName = resultSet.getString("lastName");
                    String email = resultSet.getString("email");
                    String password = resultSet.getString("password");
                    int office = resultSet.getInt("office");
                    rez = new User(id, firstName, lastName, email, password, office);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("ERROR DB" + e);
        }
        logger.traceExit(rez);
        return rez;
    }

    @Override
    public Collection<User> findAll() {
        logger.traceEntry("findall task {}");
        Connection con = dbUtils.getConnection();
        List<User> rez = new ArrayList<>();
        try (PreparedStatement preparedStatement = con.prepareStatement("select * from User")) {
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    UUID id = UUID.fromString(resultSet.getString("id"));
                    //UUID id = (UUID) resultSet.getObject("id");
                    String firstName = resultSet.getString("firstName");
                    String lastName = resultSet.getString("lastName");
                    String email = resultSet.getString("email");
                    String password = resultSet.getString("password");
                    int office = resultSet.getInt("office");
                    User usr = new User(id, firstName, lastName, email, password, office);
                    rez.add(usr);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("ERROR DB" + e);
        }
        logger.traceExit(rez);
        return rez;
    }

    @Override
    public User save(User entity) throws IllegalArgumentException {
        logger.traceEntry("saving task {}", entity);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = con.prepareStatement("insert into User(id, firstName, lastName, email, password, office) values (?,?,?,?,?,?)")) {
            preparedStatement.setObject(1, entity.getId());
            preparedStatement.setString(2, entity.getFirstName());
            preparedStatement.setString(3, entity.getLastName());
            preparedStatement.setString(4, entity.getEmail());
            preparedStatement.setString(5, entity.getPassword());
            preparedStatement.setInt(6, entity.getOffice());
            int result = preparedStatement.executeUpdate();
            logger.trace("Saved {} instances", result);
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("ERROR DB " + ex);
        }
        logger.traceExit();
        return entity;
    }

    @Override
    public User remove(UUID uuid) throws IllegalArgumentException {
        return null;
    }

    @Override
    public User update(UUID uuid, User entity) throws IllegalArgumentException {
        logger.traceEntry("update task {}", entity);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = con.prepareStatement("update User set firstName = ?, lastName = ?, email = ?, password= ?, office= ? where id = ?")) {
            preparedStatement.setString(1, entity.getFirstName());
            preparedStatement.setString(2, entity.getLastName());
            preparedStatement.setString(3, entity.getEmail());
            preparedStatement.setString(4, entity.getPassword());
            preparedStatement.setInt(5, entity.getOffice());
            preparedStatement.setObject(6, uuid);
            int result = preparedStatement.executeUpdate();
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
        return entity;
    }

    @Override
    public User findByEmailPassword(String email, String password) throws IllegalArgumentException {
        logger.traceEntry("find by email password task {}");
        Connection con = dbUtils.getConnection();
        User rez = null;
        try(PreparedStatement preparedStatement=con.prepareStatement("select * from User where email = ? and password = ?")){
            preparedStatement.setString(1, email);
            preparedStatement.setString(2, password);
            try(ResultSet resultSet=preparedStatement.executeQuery()){
                while(resultSet.next()) {
                    UUID id = UUID.fromString(resultSet.getString("id"));
                    //UUID id =(UUID) resultSet.getObject("id");
                    String firstName = resultSet.getString("firstName");
                    String lastName = resultSet.getString("lastName");
                    int office = resultSet.getInt("office");
                    rez = new User(id,firstName,lastName,email,password,office);
                }
            }
        }catch(SQLException e){
            logger.error(e);
            System.err.println("ERROR DB"+ e);
        }
        logger.traceExit(rez);
        return rez;
    }
}
