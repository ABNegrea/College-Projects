package Concurs.Repository.Interfaces;


import Concurs.Domain.Event;

import java.util.UUID;

public interface EventRepository extends Repository<UUID, Event> {
    public int addEnrolledToEvent(UUID id) throws IllegalArgumentException;

    Event findByNameAge(String name, int ageInRange) throws IllegalArgumentException;
}
