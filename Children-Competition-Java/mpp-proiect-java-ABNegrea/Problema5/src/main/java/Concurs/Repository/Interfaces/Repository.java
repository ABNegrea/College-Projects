package Concurs.Repository.Interfaces;
import Concurs.Domain.Entity;


import java.util.Collection;

public interface Repository<ID, E extends Entity<ID>> {
    E findByID(ID id) throws IllegalArgumentException;

    Collection<E> findAll();

    E save(E entity) throws IllegalArgumentException;

    E remove(ID id) throws IllegalArgumentException;

    E update(ID id, E entity) throws IllegalArgumentException;
}



