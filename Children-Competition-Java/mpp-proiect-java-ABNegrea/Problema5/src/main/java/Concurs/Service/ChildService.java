package Concurs.Service;

import Concurs.Domain.Child;
import Concurs.Repository.ChildDBRepository;

public class ChildService {
    private ChildDBRepository childRepo;

    public ChildService(ChildDBRepository childRepo) {
        this.childRepo = childRepo;
    }

    public Child addChild(Child ch){
        return this.childRepo.save(ch);
    }

    public Child findChildByNameAge(String firstName, String lastName, int age)
    {
        return this.childRepo.findChildByNameAge(firstName,lastName,age);
    }
}
