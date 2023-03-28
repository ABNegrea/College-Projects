#include "RepoOffer.h"
using namespace std;

void RepoOffer::store(const Offer& off)
{
	offers.emplace_back(off);
}

bool RepoOffer::exist(const Offer& off) const
{
	if(find(off.getName()).getName() != "")
		return true;
	return false;
}

const Offer& RepoOffer::find(string name) const 
{
	auto it = find_if(offers.begin(), offers.end(), [&](Offer x) {return x.getName() == name; });
	if(it!=offers.end())
		return *it;
	//for (const auto& y : offers) 
		//if (y.getName() == name) 
			//return y;
	return Offer("", "", "", 0);
}

const VO& RepoOffer::getAll() const noexcept 
{
	return offers;
}

void RepoOffer::setAll(VO& newoffers)
{
	offers.resize(newoffers.size());
	for (int i = 0; i < newoffers.size(); i++)
		offers[i] = newoffers[i];
}

void RepoOffer::deleteOffer(int poz)
{
	offers.erase(offers.begin() + poz);
}

void RepoOfferFile::store(const Offer& off)
{
	RepoOffer::store(off);//apelam metoda din clasa de baza
	saveToFile();
}
void RepoOfferFile::deleteOffer(int poz)
{
	RepoOffer::deleteOffer(poz);//apelam metoda din clasa de baza
	saveToFile();
}

void RepoOfferFile::loadFromFile()
{
	ifstream OfferFile(filename);
	string line;
	while (getline(OfferFile, line))
	{
		string name, destination, type;

		stringstream linestream(line);
		string current_item;

		int item_no = 0;
		int price;

		while (getline(linestream, current_item, ';'))
		{
			if (item_no == 0) name = current_item;
			if (item_no == 1) destination = current_item;
			if (item_no == 2) type = current_item;
			if (item_no == 3) price = stoi(current_item);
			item_no++;
		}
		Offer off{ name,destination,type,price };

		RepoOffer::store(off);
	}
	OfferFile.close();
}

void RepoOfferFile::saveToFile()
{
	ofstream Output(this->filename);
	for (auto& offer : getAll()) {
		Output << offer.getName() << ";" << offer.getDestination() << ";";
		Output << offer.getType() << ";" << offer.getPrice() << endl;
	}
	Output.close();
}