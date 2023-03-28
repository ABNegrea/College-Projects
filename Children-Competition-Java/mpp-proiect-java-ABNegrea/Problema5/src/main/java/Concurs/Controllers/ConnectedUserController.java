package Concurs.Controllers;

import Concurs.Domain.Child;
import Concurs.Domain.Event;
import Concurs.Domain.Participation;
import Concurs.Service.ChildService;
import Concurs.Service.EventService;
import Concurs.Service.ParticipationService;
import Concurs.Service.UserService;
import javafx.beans.property.ReadOnlyObjectWrapper;
import javafx.beans.value.ObservableValue;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.stage.Stage;
import Concurs.Controllers.LogInController;
import java.io.IOException;
import java.util.*;

public class ConnectedUserController {
    private ChildService childService;
    private EventService eventService;
    private ParticipationService participationService;
    private UserService userService;
    @FXML
    private TableView<Event> probeTable;
    @FXML
    private TableColumn<Event, String> probaColumn;
    @FXML
    private TableColumn<Event, String> categorieColumn;
    @FXML
    private TableColumn<Event, Number> inscrisiColumn;
    @FXML
    private TableView<Child> participantiTable;
    @FXML
    private TableColumn<Child, String> numeColumn;
    @FXML
    private TableColumn<Child, String> prenumeColumn;
    @FXML
    private TableColumn<Child, Number> varstaColumn;
    @FXML
    private ComboBox<String> probaInputBox;
    @FXML
    private ComboBox<String> probaSearchBox;
    @FXML
    private ComboBox<String> ageSearchBox;
    @FXML
    private TextField childAgeField;
    @FXML
    private TextField childNameField;

    @FXML
    private Label errorLabel;

    public void setAll(ChildService cs, EventService es, ParticipationService ps, UserService us){
        this.childService = cs;
        this.eventService = es;
        this.participationService = ps;
        this.userService = us;
        loadEvents();

        List<String> eventNames = new ArrayList<>();
        List<String> eventCategories = new ArrayList<>();
        for(Event e: es.findAll()) {
            eventNames.add(e.getName());
            eventCategories.add(e.AgeToString());
        }
        Collections.sort(eventNames);
        Collections.sort(eventCategories);
        Set<String> uniqueEvents = new HashSet<>(eventNames);
        Set<String> uniqueCategories = new HashSet<>(eventCategories);
        probaInputBox.getItems().addAll(uniqueEvents);
        probaSearchBox.getItems().addAll(uniqueEvents);
        ageSearchBox.getItems().addAll(uniqueCategories);
    }
    void loadEvents() {
        probeTable.getItems().clear();
        categorieColumn.setStyle( "-fx-alignment: CENTER;");
        probaColumn.setStyle( "-fx-alignment: CENTER;");
        inscrisiColumn.setStyle( "-fx-alignment: CENTER;");

        categorieColumn.setCellValueFactory(p -> new ReadOnlyObjectWrapper<>(p.getValue().AgeToString()));
        probaColumn.setCellValueFactory(new PropertyValueFactory<>("name"));
        inscrisiColumn.setCellValueFactory(new PropertyValueFactory<>("enrolled"));

        ObservableList<Event> eventObservableList = FXCollections.observableArrayList(eventService.findAll());
        for (Event e : eventObservableList)
            probeTable.getItems().add(e);
    }

    @FXML
    void searchParticipants(){
        participantiTable.getItems().clear();
        numeColumn.setStyle( "-fx-alignment: CENTER;");
        prenumeColumn.setStyle( "-fx-alignment: CENTER;");
        varstaColumn.setStyle( "-fx-alignment: CENTER;");

        varstaColumn.setCellValueFactory(new PropertyValueFactory<>("age"));
        prenumeColumn.setCellValueFactory(new PropertyValueFactory<>("lastName"));
        numeColumn.setCellValueFactory(new PropertyValueFactory<>("firstName"));

        ObservableList<Participation> participationObservableList = FXCollections.observableArrayList(participationService.findAll());
        for(Participation p: participationObservableList)
            if(p.getEvent().AgeToString().equals(ageSearchBox.getSelectionModel().getSelectedItem()) &&
            p.getEvent().getName().equals(probaSearchBox.getSelectionModel().getSelectedItem()))
                participantiTable.getItems().add(p.getChild());
    }

    @FXML
    void addParticipant(){
        String childName[] = childNameField.getText().split(" ", 2);
        int childAge = Integer.parseInt(childAgeField.getText());

        Child childTest = childService.findChildByNameAge(childName[0], childName[1], childAge);
        if (childTest == null) {
            Child child = new Child(childName[0], childName[1], childAge);
            childService.addChild(child);
            Event evnt = eventService.findByNameAge(probaInputBox.getSelectionModel().getSelectedItem(), Integer.parseInt(childAgeField.getText()));
            Participation participation = new Participation(child, evnt);
            participationService.addParticipation(participation);
            eventService.addEnrolledToEvent(evnt.getId());
            errorLabel.setVisible(false);
        } else {
            int countParticipations = participationService.participationCountChild(childTest.getId());
            if (countParticipations >= 2) {
                errorLabel.setText("Copilul participa deja la 2 probe");
                errorLabel.setVisible(true);
            } else {
                Event evnt = eventService.findByNameAge(probaInputBox.getSelectionModel().getSelectedItem(), Integer.parseInt(childAgeField.getText()));
                Participation participation = new Participation(childTest, evnt);
                participationService.addParticipation(participation);
                eventService.addEnrolledToEvent(evnt.getId());
                errorLabel.setVisible(false);
            }
        }

        loadEvents();
    }
    @FXML
    void logOut() throws IOException {
        FXMLLoader fxmlLoader = new FXMLLoader();
        fxmlLoader.setLocation(getClass().getResource("/Views/LogIn.fxml"));
        Stage stage = new Stage();
        stage.setTitle("Login");
        stage.setScene(new Scene(fxmlLoader.load()));
        LogInController loginController = fxmlLoader.getController();
        loginController.setServices(childService,eventService,participationService,userService);
        probeTable.getScene().getWindow().hide();
        stage.show();
    }

}
