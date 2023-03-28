package Concurs;

import Concurs.Controllers.LogInController;
import Concurs.Domain.Child;
import Concurs.Domain.User;
import Concurs.Repository.ChildDBRepository;
import Concurs.Repository.EventDBRepository;
import Concurs.Repository.ParticipationDBRepository;
import Concurs.Repository.UserDBRepository;
import Concurs.Service.ChildService;
import Concurs.Service.EventService;
import Concurs.Service.ParticipationService;
import Concurs.Service.UserService;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.stage.Stage;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;
import java.util.UUID;

public class MainApplication extends Application {
    @Override
    public void start(Stage primaryStage) throws Exception {
        FXMLLoader fxmlLoader =new FXMLLoader(MainApplication.class.getResource("/Views/LogIn.fxml"));
        Scene scene = new Scene(fxmlLoader.load());
        primaryStage.setTitle("Login");
        primaryStage.setScene(scene);

        Properties properties = new Properties();
        try{
            properties.load(new FileReader("db.config.properties"));
        }
        catch (IOException e){
            System.out.println("Cannot find file");
        }

        UserDBRepository userRepo = new UserDBRepository(properties);
        EventDBRepository eventRepo = new EventDBRepository(properties);
        ChildDBRepository childRepo = new ChildDBRepository(properties);
        ParticipationDBRepository participationRepo = new ParticipationDBRepository(properties,eventRepo,childRepo);

        UserService userService = new UserService(userRepo);
        EventService eventService = new EventService(eventRepo);
        ChildService childService = new ChildService(childRepo);
        ParticipationService participationService = new ParticipationService(participationRepo);

        LogInController logInController = fxmlLoader.getController();
        logInController.setServices(childService,eventService,participationService,userService);
        primaryStage.show();
    }

    public static void main(String[] args) {
        Properties properties = new Properties();
        try{
            properties.load(new FileReader("db.config.properties"));
        }
        catch (IOException e){
            System.out.println("Cannot find file");
        }
        EventDBRepository eventDBRepository = new EventDBRepository(properties);
        EventService eventService = new EventService(eventDBRepository);
        System.out.println(eventService.findByNameAge("Poezie",7).getMaxAge());
        launch(args);
    }
}