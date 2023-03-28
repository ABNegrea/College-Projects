package Concurs.Service;

import Concurs.Domain.Event;
import Concurs.Repository.EventDBRepository;

import java.util.List;
import java.util.UUID;

public class EventService {
    private EventDBRepository eventRepo;

    public EventService(EventDBRepository eventRepo) {
        this.eventRepo = eventRepo;
    }

    public List<Event> findAll()
    {
        return eventRepo.findAll().stream().toList();
    }

    public Event findByNameAge(String name, int ageInRange){
        return eventRepo.findByNameAge(name,ageInRange);
    }

    public int addEnrolledToEvent(UUID id){
        return this.eventRepo.addEnrolledToEvent(id);
    }
}
