#include "Offer.h"

int Offer::getPrice() const noexcept
{
	return price;
}
string Offer::getType() const
{
	return type;
}
string Offer::getDestination() const
{
	return destination;
}
string Offer::getName() const
{
	return name;
}

void Offer::setName(string newname)
{
	name = newname;
}

void Offer::setType(string newtype)
{
	type = newtype;
}

void Offer::setDestination(string newdestination)
{
	destination = newdestination;
}

void Offer::setPrice(int newprice) noexcept
{
	price = newprice;
}

bool cmpName(const Offer& off1, const Offer& off2)
{
	return off1.getName() < off2.getName();
}

bool cmpDestination(const Offer& off1, const Offer& off2)
{
	return off1.getDestination() < off2.getDestination();
}

bool cmpTypePrice(const Offer& off1, const Offer& off2)
{
	if(off1.getType() == off2.getType())
		return off1.getPrice() < off2.getPrice();
	return off1.getType() < off2.getType();
}