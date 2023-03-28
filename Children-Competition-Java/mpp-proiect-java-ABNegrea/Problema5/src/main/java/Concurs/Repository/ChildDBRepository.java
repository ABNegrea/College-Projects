package Concurs.Repository;

import Concurs.Domain.Child;
import Concurs.Domain.Event;
import Concurs.Repository.Interfaces.ChildRepository;
import Concurs.Repository.Interfaces.JdbcUtils;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.io.FileReader;
import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.*;

public class ChildDBRepository implements ChildRepository {
    private JdbcUtils dbUtils;
    private static final Logger logger = LogManager.getLogger();

    public ChildDBRepository(Properties props) {
        logger.info("Initializing ChildDBRepository with properties: {} ", props);
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public Child findByID(UUID uuid) throws IllegalArgumentException {
        logger.traceEntry("find by id task {}");
        Connection con = dbUtils.getConnection();
        Child rez = null;
        try (PreparedStatement preparedStatement = con.prepareStatement("select * from Child where id = ?")) {
            preparedStatement.setObject(1, uuid);
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    UUID id = UUID.fromString(resultSet.getString("id"));
                    //UUID id = (UUID) resultSet.getObject("id");
                    String firstName = resultSet.getString("firstName");
                    String lastName = resultSet.getString("lastName");
                    int age = resultSet.getInt("age");
                    rez = new Child(id, firstName, lastName, age);
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
    public Collection<Child> findAll() {
        logger.traceEntry("findall task {}");
        Connection con = dbUtils.getConnection();
        List<Child> rez = new ArrayList<>();
        try (PreparedStatement preparedStatement = con.prepareStatement("select * from Child")) {
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    UUID id = UUID.fromString(resultSet.getString("id"));
                    //UUID id = (UUID) resultSet.getObject("id");
                    String firstName = resultSet.getString("firstName");
                    String lastName = resultSet.getString("lastName");
                    int age = resultSet.getInt("age");
                    Child chd = new Child(id, firstName, lastName, age);
                    rez.add(chd);
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
    public Child save(Child entity) throws IllegalArgumentException {
        logger.traceEntry("saving task {}", entity);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = con.prepareStatement("insert into Child(id, firstName, lastName, age) values (?,?,?,?)")) {
            preparedStatement.setObject(1, entity.getId());
            preparedStatement.setString(2, entity.getFirstName());
            preparedStatement.setString(3, entity.getLastName());
            preparedStatement.setInt(4, entity.getAge());
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
    public Child remove(UUID uuid) throws IllegalArgumentException {
        return null;
    }

    @Override
    public Child update(UUID uuid, Child entity) throws IllegalArgumentException {
        logger.traceEntry("update task {}", entity);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = con.prepareStatement("update User set firstName = ?, lastName = ?, age = ? where id = ?")) {
            preparedStatement.setString(1, entity.getFirstName());
            preparedStatement.setString(2, entity.getLastName());
            preparedStatement.setInt(3, entity.getAge());
            preparedStatement.setObject(4, uuid);
            int result = preparedStatement.executeUpdate();
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
        return entity;
    }

    public void addChildren()
    {
        Properties properties = new Properties();
        try{
            properties.load(new FileReader("db.config.properties"));
        }
        catch (IOException e){
            System.out.println("Cannot find file");
        }
        ChildDBRepository childDBRepository = new ChildDBRepository(properties);
        Child child1 = new Child("Razvan","Constantinescu",6);
        Child child2 = new Child("Daniel","Sofran",7);
        Child child3 = new Child("Alex","Mocanu",8);
        Child child4 = new Child("Andrei","Muresanu",9);
        Child child5 = new Child("Rares","Perta",10);
        Child child6 = new Child("Andreea","Oniga",11);
        Child child7 = new Child("Sabina","Muresan",12);
        Child child8 = new Child("Ecaterina","Munteanu",13);
        Child child9 = new Child("Stefan","Nastasa",14);
        Child child10 = new Child("Ion","Popescu",15);
        Child child0 = new Child("Nicu","Pop",15);


        childDBRepository.save(child1);
        childDBRepository.save(child2);
        childDBRepository.save(child3);
        childDBRepository.save(child4);
        childDBRepository.save(child5);
        childDBRepository.save(child6);
        childDBRepository.save(child7);
        childDBRepository.save(child8);
        childDBRepository.save(child9);
        childDBRepository.save(child10);
        childDBRepository.save(child0);
    }

    @Override
    public Child findChildByNameAge(String firstName, String lastName, int age) {
        logger.traceEntry("find by name and age task {}");
        Connection con = dbUtils.getConnection();
        Child rez = null;
        try (PreparedStatement preparedStatement = con.prepareStatement("select * from Child where firstName = ? and lastName=? and age=? ")) {
            preparedStatement.setString(1, firstName);
            preparedStatement.setString(2,lastName);
            preparedStatement.setInt(3,age);
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    UUID id = UUID.fromString(resultSet.getString("id"));
                    //UUID id = (UUID) resultSet.getObject("id");
                    String firstNameRez = resultSet.getString("firstName");
                    String lastNameRez =  resultSet.getString("lastName");
                    int ageRez = resultSet.getInt("age");
                    rez = new Child(id, firstNameRez, lastNameRez, ageRez);
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
