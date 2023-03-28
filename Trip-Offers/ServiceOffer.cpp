#include "ServiceOffer.h"
#include <algorithm>
#include <vector>
using namespace std;



const VO& ServiceOffer::getAll() noexcept
{
	return rep.getAll();
}

void ServiceOffer::addOffer(const string& name, const string& destination, const string& type, int price)
{
	Offer off{ name,destination,type,price };
	val.validate(off);
	undoList.list.emplace_back(rep.getAll(), "adauga");
	rep.store(off);
}

VO ServiceOffer::sortByName() 
{
	auto copyAll = rep.getAll();
	sort(copyAll.begin(), copyAll.end(), cmpName);
	return copyAll;
}

VO ServiceOffer::sortByDestination()
{
	auto copyAll = rep.getAll();
	sort(copyAll.begin(), copyAll.end(), cmpDestination);
	return copyAll;
}

VO ServiceOffer::sortByTypePrice()
{
	auto copyAll = rep.getAll();
	sort(copyAll.begin(), copyAll.end(), cmpTypePrice);
	return copyAll;
}

VO ServiceOffer::filterPrice(int price)
{
	VO filtered;
	auto it = copy_if(rep.getAll().begin(), rep.getAll().end(), back_inserter(filtered), [&](Offer i) {return i.getPrice() < price; });
	//for (auto& y : rep.getAll())
		//if (y.getPrice() < price)
		//	filtered.emplace_back(y);
	return filtered;
}

VO ServiceOffer::filterDestination(const string& destination)
{
	VO filtered;
	auto it = copy_if(rep.getAll().begin(), rep.getAll().end(), back_inserter(filtered), [&](Offer i) {return i.getDestination() == destination; });
	//for (auto& y : rep.getAll())
		//if (y.getDestination() == destination)
			//filtered.emplace_back(y);
	return filtered;
}

const Offer ServiceOffer::searchOffer(const string& name)
{
	Offer aux = Offer("", "", "", -1);
	aux = rep.find(name);
	return aux;
}

void ServiceOffer::deleteOfferName(const string& name)
{
	undoList.list.emplace_back(rep.getAll(), "sterge");
	for (int i = 0; i < rep.getAll().size(); i++)
		if (rep.getAll()[i].getName() == name)
			rep.deleteOffer(i);
}

void ServiceOffer::modifyOffer(const string& name, const string& newname, const string& newdestination, const string& newtype, int newprice)
{
	VO modified = rep.getAll();
	undoList.list.emplace_back(rep.getAll(), "modifica");
	for(auto& y :modified)
		if (y.getName() == name)
		{
			y.setName(newname);
			y.setPrice(newprice);
			y.setDestination(newdestination);
			y.setType(newtype);
		}
	rep.setAll(modified);
}

void ServiceOffer::setAll(VO& newoffers)
{
	rep.setAll(newoffers);
}

void ServiceOffer::undo(const string& command)
{
	if (!undoList.list.empty())
	{
		if (command == "adauga")
		{
			UndoAdauga aux;
			aux.list = undoList.list;
			aux.doUndo(rep);
			undoList.list = aux.list;
		}
		else if (command == "sterge")
		{
			UndoSterge aux;
			aux.list = undoList.list;
			aux.doUndo(rep);
			undoList.list = aux.list;
		}
		else if(command == "modifica")
		{
			UndoModifica aux;
			aux.list = undoList.list;
			aux.doUndo(rep);
			undoList.list = aux.list;
		}
	}
}
