package Concurs.Service;

import Concurs.Domain.Participation;
import Concurs.Repository.ParticipationDBRepository;

import java.util.List;
import java.util.UUID;

public class ParticipationService {
    private final ParticipationDBRepository participationRepo;

    public ParticipationService(ParticipationDBRepository participationRepo) {
        this.participationRepo = participationRepo;
    }
    public Participation addParticipation(Participation participation){
        return this.participationRepo.save(participation);
    }

    public List<Participation> findAll()
    {
        return participationRepo.findAll().stream().toList();
    }

    public int participationCountChild(UUID id){
        return this.participationRepo.participationCountChild(id);
    }
}
