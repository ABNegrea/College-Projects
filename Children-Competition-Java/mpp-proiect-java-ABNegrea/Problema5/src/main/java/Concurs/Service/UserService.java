package Concurs.Service;

import Concurs.Domain.User;
import Concurs.Repository.UserDBRepository;

public class UserService {
    private UserDBRepository userRepo;

    public UserService(UserDBRepository userRepo) {
        this.userRepo = userRepo;
    }

    public User findByEmailPassword(String email, String password) throws Exception {
        User user = userRepo.findByEmailPassword(email,password);
        if(user == null)
            throw new Exception("User inexistent");
        return user;

    }
}
