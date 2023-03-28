package Concurs.Repository;

import Concurs.Domain.Event;
import Concurs.Repository.Interfaces.EventRepository;
import Concurs.Repository.Interfaces.JdbcUtils;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.*;

public class EventDBRepository implements EventRepository {
    private JdbcUtils dbUtils;
    private static final Logger logger = LogManager.getLogger();

    public EventDBRepository(Properties props) {
        logger.info("Initializing EventDBRepository with properties: {} ", props);
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public Event findByID(UUID uuid) throws IllegalArgumentException {
        logger.traceEntry("find by id task {}");
        Connection con = dbUtils.getConnection();
        Event rez = null;
        try (PreparedStatement preparedStatement = con.prepareStatement("select * from Event where id = ?")) {
            preparedStatement.setObject(1, uuid);
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    UUID id = UUID.fromString(resultSet.getString("id"));
                    //UUID id = (UUID) resultSet.getObject("id");
                    String name = resultSet.getString("name");
                    int minAge = resultSet.getInt("minAge");
                    int maxAge = resultSet.getInt("maxAge");
                    int enrolled = resultSet.getInt("enrolled");
                    rez = new Event(id, name, minAge, maxAge, enrolled);
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
    public Collection<Event> findAll() {
        logger.traceEntry("findall task {}");
        Connection con = dbUtils.getConnection();
        List<Event> rez = new ArrayList<>();
        try (PreparedStatement preparedStatement = con.prepareStatement("select * from Event")) {
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    UUID id = UUID.fromString(resultSet.getString("id"));
                    //UUID id = (UUID) resultSet.getObject("id");
                    String name = resultSet.getString("name");
                    int minAge = resultSet.getInt("minAge");
                    int maxAge = resultSet.getInt("maxAge");
                    int enrolled = resultSet.getInt("enrolled");
                    Event evt = new Event(id, name, minAge, maxAge, enrolled);
                    rez.add(evt);
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
    public Event save(Event entity) throws IllegalArgumentException {
        logger.traceEntry("saving task {}", entity);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = con.prepareStatement("insert into Event(id, name, minAge, maxAge, enrolled) values (?,?,?,?,?)")) {
            preparedStatement.setObject(1, entity.getId());
            preparedStatement.setString(2, entity.getName());
            preparedStatement.setInt(3, entity.getMinAge());
            preparedStatement.setInt(4, entity.getMaxAge());
            preparedStatement.setInt(5, entity.getEnrolled());
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
    public Event remove(UUID uuid) throws IllegalArgumentException {
        return null;
    }

    @Override
    public Event update(UUID uuid, Event entity) throws IllegalArgumentException {
        logger.traceEntry("update task {}", entity);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = con.prepareStatement("update Event set name = ?, minAge = ?, maxAge = ?, enrolled=? where id = ?")) {
            preparedStatement.setString(1, entity.getName());
            preparedStatement.setInt(2, entity.getMinAge());
            preparedStatement.setInt(3, entity.getMaxAge());
            preparedStatement.setInt(4, entity.getEnrolled());
            preparedStatement.setObject(5, uuid);
            int result = preparedStatement.executeUpdate();
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
        return entity;
    }

    @Override
    public int addEnrolledToEvent(UUID id) throws IllegalArgumentException {
        logger.traceEntry("update task {}");
        Connection con = dbUtils.getConnection();
        int en = this.findByID(id).getEnrolled() + 1;
        try (PreparedStatement preparedStatement = con.prepareStatement("update Event set enrolled=? where id = ?")) {
            preparedStatement.setInt(1, en);
            preparedStatement.setObject(2, id);
            int result = preparedStatement.executeUpdate();
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
        return en;
    }

    @Override
    public Event findByNameAge(String name, int ageInRange) throws IllegalArgumentException {
        logger.traceEntry("find by name and age task {}");
        Connection con = dbUtils.getConnection();
        Event rez = null;
        try (PreparedStatement preparedStatement = con.prepareStatement("select * from Event where name = ? and minAge<=? and maxAge>=? ")) {
            preparedStatement.setString(1, name);
            preparedStatement.setInt(2,ageInRange);
            preparedStatement.setInt(3,ageInRange);
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    UUID id = UUID.fromString(resultSet.getString("id"));
                    //UUID id = (UUID) resultSet.getObject("id");
                    String namerez = resultSet.getString("name");
                    int minAgerez = resultSet.getInt("minAge");
                    int maxAgerez = resultSet.getInt("maxAge");
                    int enrolled = resultSet.getInt("enrolled");
                    rez = new Event(id, namerez, minAgerez, maxAgerez, enrolled);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("ERROR DB" + e);
        }
        logger.traceExit(rez);
        return rez;
    }
}
