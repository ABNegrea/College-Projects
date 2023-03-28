package Concurs.Domain;

import java.util.UUID;

public class Event extends Entity<UUID>{
    private String name;
    private int minAge;
    private int maxAge;

    private int enrolled;

    public Event(String name, int minAge, int maxAge, int enrolled) {
        this.setId(UUID.randomUUID());
        this.name = name;
        this.minAge = minAge;
        this.maxAge = maxAge;
        this.enrolled = enrolled;
    }

    public Event(UUID id, String name, int minAge, int maxAge, int enrolled) {
        this.setId(id);
        this.name = name;
        this.minAge = minAge;
        this.maxAge = maxAge;
        this.enrolled = enrolled;
    }

    public String AgeToString(){
        return String.valueOf(this.minAge) + "-" + String.valueOf(this.maxAge);
    }


    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getMinAge() {
        return minAge;
    }

    public void setMinAge(int minAge) {
        this.minAge = minAge;
    }

    public int getMaxAge() {
        return maxAge;
    }

    public void setMaxAge(int maxAge) {
        this.maxAge = maxAge;
    }

    public int getEnrolled() {
        return enrolled;
    }

    public void setEnrolled(int enrolled) {
        this.enrolled = enrolled;
    }
}
