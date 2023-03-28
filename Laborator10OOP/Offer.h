#pragma once
#include <string>
#include <iostream>
using namespace std;

class Offer
{
private:
	string name;
	string destination;
	string type;
	int price;

public:
	Offer() = default;
	// Creates a new offer
	Offer(string name, string destination, string type, int price) :name{ name }, destination{ destination }, type{ type }, price{ price } {}

	Offer(const Offer& ot) :name{ ot.name }, destination{ ot.destination }, type{ ot.type }, price{ ot.price } {
		//cout << "!!!!!!!!!!!!!!!!!!!!!!!!!!\n";
	}


	// Gets the price of an offer
	int getPrice() const noexcept;

	// Gets the type of an offer
	string getType() const;

	// Gets the destination of an offer
	string getDestination() const;

	// Gets the name of an offer
	string getName() const;

	// Sets a new name
	void setName(string newname);

	// Sets a new type
	void setType(string newtype);

	// Sets a new destination
	void setDestination(string newdestination);

	// Sets a new price
	void setPrice(int newprice) noexcept;

};

// Compares the names of 2 offers
bool cmpName(const Offer& off1, const Offer& off2);

// Compares the destination of 2 offers 
bool cmpDestination(const Offer& off1, const Offer& off2);

// Compares the type and then the price of 2 offers 
bool cmpTypePrice(const Offer& off1, const Offer& off2) ;

