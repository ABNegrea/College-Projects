module Concurs {
    requires javafx.controls;
    requires javafx.fxml;
    requires org.apache.logging.log4j;
    requires java.sql;

    exports Concurs.Domain;
    exports Concurs.Controllers;
    exports Concurs;

    opens Concurs to javafx.fxml;
    opens Concurs.Controllers to javafx.fxml;
    opens Concurs.Domain to javafx.fxml;


}