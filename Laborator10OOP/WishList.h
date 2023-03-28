#pragma once
#include "Offer.h"
#include <vector>
#include <algorithm>
#include <random>
#include <fstream>
#include <sstream>
#include <chrono>
using namespace std;
using VO = vector<Offer>;

class WishList
{
private:
	VO wishlist;

public:
	// Constructor
	WishList() = default;
	
	// Empty the wishlist
	virtual void emptyWishList() noexcept;

	// Adds an offer to the wishlist
	virtual void addOfferWishList(const Offer& off);

	// Adds nr random offers to the wishlist
	virtual void addRandomOffers(VO offers, int nr);

	const VO& getAll() noexcept;

	// exports the wishlist to a file with a given name
	void exportFile(const string& file);
};

class WishListFile :public WishList
{
private:
	string filename;
	void loadFromFile();
	void saveToFile();
public:
	WishListFile(std::string fName) :WishList(), filename{ fName }
	{
		loadFromFile();//incarcam datele din fisier
	}
	void addOfferWishList(const Offer& off) override;
	void addRandomOffers(VO offers, int nr) override;
	void emptyWishList() noexcept override;
};