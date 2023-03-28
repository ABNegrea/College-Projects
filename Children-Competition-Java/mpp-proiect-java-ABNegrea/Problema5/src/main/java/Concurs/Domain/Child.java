package Concurs.Domain;

import java.util.UUID;

public class Child extends Entity<UUID> {
    private String firstName;
    private String lastName;
    private int age;

    public Child(String firstName, String lastName, int age) {
        this.setId(UUID.randomUUID());
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
    }

    public Child(UUID id, String firstName, String lastName, int age) {
        this.setId(id);
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }
}
