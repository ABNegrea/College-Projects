#pragma once
#include "Offer.h"
#include <vector>
#include <fstream>
#include <sstream>
using namespace std;
using VO = vector<Offer>;

class RepoOffer
{
private:
	VO offers;

public:
	RepoOffer() = default;

	// Method for verifying if the offer already exists
	bool exist(const Offer& off) const;

	// Doesn't allow copying data
	RepoOffer(const RepoOffer& ot) = delete;

	// Stores offers
	virtual void store(const Offer& off);

	// Searches an offer by it's name
	const Offer& find(string name) const;

	// Returns all stored offers
	const VO& getAll() const noexcept;

	// Changes the repo
	void setAll(VO& newoffers);

	virtual void deleteOffer(int poz);
};


class RepoOfferFile :public RepoOffer 
{
private:
	string filename;
	void loadFromFile();
	void saveToFile();
public:
	RepoOfferFile(std::string fName) :RepoOffer(), filename{ fName }
	{
		loadFromFile();//incarcam datele din fisier
	}
	void store(const Offer& off) override;
	void deleteOffer(int poz) override;
};