package Concurs.Repository.Interfaces;

import Concurs.Domain.Child;

import java.util.UUID;

public interface ChildRepository extends Repository<UUID, Child> {
    public Child findChildByNameAge(String firstName, String lastName, int age);
}
