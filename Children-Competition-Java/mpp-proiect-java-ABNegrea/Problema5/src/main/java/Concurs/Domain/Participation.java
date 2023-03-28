package Concurs.Domain;

import java.util.UUID;

public class Participation extends Entity<UUID>{
    private Child child;
    private Event event;

    public Participation(UUID id, Child ch, Event ev) {
        this.setId(id);
        this.child = ch;
        this.event = ev;
    }

    public Participation(Child ch, Event ev) {
        this.setId(UUID.randomUUID());;
        this.child = ch;
        this.event = ev;
    }

    public Child getChild() {
        return child;
    }

    public void setChild(Child child) {
        this.child = child;
    }

    public Event getEvent() {
        return event;
    }

    public void setEvent(Event event) {
        this.event = event;
    }
}
