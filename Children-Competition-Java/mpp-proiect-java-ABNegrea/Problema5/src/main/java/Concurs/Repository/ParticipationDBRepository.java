package Concurs.Repository;

import Concurs.Domain.Participation;
import Concurs.Repository.Interfaces.JdbcUtils;
import Concurs.Repository.Interfaces.ParticipationRepository;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.*;

public class ParticipationDBRepository implements ParticipationRepository {
    private JdbcUtils dbUtils;
    private static final Logger logger= LogManager.getLogger();

    private EventDBRepository eventRepo;

    private ChildDBRepository childRepo;

    public ParticipationDBRepository(Properties props, EventDBRepository repo1, ChildDBRepository repo2) {
        logger.info("Initializing Participation :) DBRepository with properties: {} ",props);
        dbUtils = new JdbcUtils(props);
        eventRepo = repo1;
        childRepo = repo2;
    }

    @Override
    public Participation findByID(UUID uuid) throws IllegalArgumentException {
        logger.traceEntry("find by id task {}");
        Connection con = dbUtils.getConnection();
        Participation rez = null;
        try(PreparedStatement preparedStatement=con.prepareStatement("select * from Participation where id = ?")){
            preparedStatement.setObject(1, uuid);
            try(ResultSet resultSet=preparedStatement.executeQuery()){
                while(resultSet.next()) {
                    UUID id = UUID.fromString(resultSet.getString("id"));
                    //UUID id = (UUID) resultSet.getObject("id");
                    UUID idChild = (UUID) resultSet.getObject("id");
                    UUID idEvent = (UUID) resultSet.getObject("id");
                    rez = new Participation(id,childRepo.findByID(idChild),eventRepo.findByID(idEvent));
                }
            }
        }catch(SQLException e){
            logger.error(e);
            System.err.println("ERROR DB"+ e);
        }
        logger.traceExit(rez);
        return rez;
    }

    @Override
    public Collection<Participation> findAll() {
        logger.traceEntry("findall task {}");
        Connection con = dbUtils.getConnection();
        List<Participation> rez = new ArrayList<>();
        try (PreparedStatement preparedStatement = con.prepareStatement("select * from Participation")) {
            try (ResultSet resultSet = preparedStatement.executeQuery()) {
                while (resultSet.next()) {
                    UUID id = UUID.fromString(resultSet.getString("id"));
                    //UUID id = (UUID) resultSet.getObject("id");
                    UUID idChild = UUID.fromString(resultSet.getString("idChild"));
                    UUID idEvent = UUID.fromString(resultSet.getString("idEvent"));
                    Participation par = new Participation(id,childRepo.findByID(idChild),eventRepo.findByID(idEvent));
                    rez.add(par);
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
    public Participation save(Participation entity) throws IllegalArgumentException {
        logger.traceEntry("saving task {}", entity);
        Connection con= dbUtils.getConnection();
        try(PreparedStatement preparedStatement=con.prepareStatement("insert into Participation(id, idChild, idEvent) values (?,?,?)")){
            preparedStatement.setObject(1, entity.getId());
            preparedStatement.setObject(2, entity.getChild().getId());
            preparedStatement.setObject(3, entity.getEvent().getId());
            int result=preparedStatement.executeUpdate();
            logger.trace("Saved {} instances",result);
        }
        catch(SQLException ex){
            logger.error(ex);
            System.err.println("ERROR DB "+ex);
        }
        logger.traceExit();
        return entity;
    }

    @Override
    public Participation remove(UUID uuid) throws IllegalArgumentException {
        return null;
    }

    @Override
    public Participation update(UUID uuid, Participation entity) throws IllegalArgumentException {
        logger.traceEntry("update task {}", entity);
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preparedStatement = con.prepareStatement("update Participation set idChild = ?, idEvent = ? where id = ?")) {
            preparedStatement.setObject(1, entity.getChild().getId());
            preparedStatement.setObject(2, entity.getEvent().getId());
            preparedStatement.setObject(3, uuid);
            int result = preparedStatement.executeUpdate();
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
        return entity;
    }

    @Override
    public int participationCountChild(UUID idChild) {
        logger.traceEntry("find by idChild task {}");
        Connection con = dbUtils.getConnection();
        int cnt = 0;
        try(PreparedStatement preparedStatement=con.prepareStatement("select * from Participation where idChild = ?")){
            preparedStatement.setObject(1, idChild);
            try(ResultSet resultSet=preparedStatement.executeQuery()){
                while(resultSet.next()) {
                    cnt = cnt + 1;
                }
            }
        }catch(SQLException e){
            logger.error(e);
            System.err.println("ERROR DB"+ e);
        }
        logger.traceExit(cnt);
        return cnt;
    }
}
