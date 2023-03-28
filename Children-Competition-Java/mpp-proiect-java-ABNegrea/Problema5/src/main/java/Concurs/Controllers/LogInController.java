package Concurs.Controllers;

import Concurs.Domain.User;
import Concurs.Service.ChildService;
import Concurs.Service.EventService;
import Concurs.Service.ParticipationService;
import Concurs.Service.UserService;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.stage.Stage;

import java.io.IOException;

public class LogInController {
    private ChildService childService;
    private EventService eventService;
    private ParticipationService participationService;
    private UserService userService;

    @FXML
    TextField emailField;

    @FXML
    PasswordField passwordField;

    @FXML
    Label errorLabel;

    @FXML
    Button connectButton;

    public void setServices(ChildService cs, EventService es, ParticipationService ps, UserService us){
        this.childService = cs;
        this.eventService = es;
        this.participationService = ps;
        this.userService = us;
    }

    public void handleLogin(ActionEvent event) throws Exception {
        String email = emailField.getText();
        String password = passwordField.getText();
        try{
            User usr = userService.findByEmailPassword(email,password);
            System.out.println("bravo patratel");
            switchToConnected();
        }
        catch(Exception e)
        {
            System.out.println(e);
            errorLabel.setVisible(true);
        }
    }

    private void switchToConnected() throws IOException{
        FXMLLoader fxmlLoader = new FXMLLoader();
        fxmlLoader.setLocation(getClass().getResource("/Views/ConnectedUser.fxml"));
        Stage stage = new Stage();
        stage.setTitle("ConnectedUser");
        stage.setScene(new Scene(fxmlLoader.load()));
        ConnectedUserController connectedUserController = fxmlLoader.getController();
        connectedUserController.setAll(childService,eventService,participationService,userService);
        errorLabel.getScene().getWindow().hide();
        stage.show();
    }
}
