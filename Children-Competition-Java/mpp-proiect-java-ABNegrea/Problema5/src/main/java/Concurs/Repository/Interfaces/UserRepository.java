package Concurs.Repository.Interfaces;

import Concurs.Domain.User;

import java.util.UUID;

public interface UserRepository extends Repository<UUID, User> {
    User findByEmailPassword(String email, String password) throws IllegalArgumentException;
}
