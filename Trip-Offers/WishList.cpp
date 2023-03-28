#include "WishList.h"
#include <fstream>
using namespace std;
using std::shuffle;

void WishList::emptyWishList() noexcept
{
	wishlist.clear();
}

const VO& WishList::getAll() noexcept
{
	return wishlist;
}

void WishList::addOfferWishList(const Offer& off)
{
	wishlist.emplace_back(off);
}

void WishList::addRandomOffers(VO v, int nr)
{
	shuffle(v.begin(), v.end(), default_random_engine(std::random_device{}()));
	while (wishlist.size() < nr && v.size() > 0)
	{
		wishlist.emplace_back(v.back());
		v.pop_back();
	}
}

void WishList::exportFile(const string& file)
{
	ofstream fin(file);
	for (auto& y : wishlist)
	{
		fin << y.getName() << " | " << y.getDestination() << " | " << y.getType() << " | " << y.getPrice() << "<br/>";
	}
	fin.close();
}

void WishListFile::loadFromFile()
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

		WishList::addOfferWishList(off);
	}
	OfferFile.close();
}

void WishListFile::saveToFile()
{
	ofstream Output(this->filename);
	for (auto& offer : getAll()) {
		Output << offer.getName() << ";" << offer.getDestination() << ";";
		Output << offer.getType() << ";" << offer.getPrice() << endl;
	}
	Output.close();
}

void WishListFile::addOfferWishList(const Offer& off)
{
	WishList::addOfferWishList(off);//apelam metoda din clasa de baza
	saveToFile();
}
void WishListFile::addRandomOffers(VO offers, int nr)
{
	WishList::addRandomOffers(offers, nr);
	saveToFile();
}
void WishListFile::emptyWishList() noexcept
{
	WishList::emptyWishList();
	saveToFile();

}