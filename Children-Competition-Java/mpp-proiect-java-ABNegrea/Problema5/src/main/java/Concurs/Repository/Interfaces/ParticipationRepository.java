package Concurs.Repository.Interfaces;
import Concurs.Domain.Participation;

import java.util.List;
import java.util.UUID;

public interface ParticipationRepository extends Repository<UUID, Participation> {
    public int participationCountChild(UUID idChild);
}
