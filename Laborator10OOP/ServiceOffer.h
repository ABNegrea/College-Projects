#pragma once
#include "RepoOffer.h"
#include "Validators.h"
#include "ActiuneUndo.h"

class ServiceOffer
{
	RepoOffer& rep;
	ActiuneUndo& undoList;
	OfferValidator val;

public:
	// Constructor
	ServiceOffer(RepoOffer& rep, ActiuneUndo& undoList, OfferValidator& val) noexcept :rep{ rep }, undoList{ undoList }, val{ val } {}

	// Returns all offers
	const VO& getAll() noexcept;

	// Adds a new offer
	void addOffer(const string& name, const string& destination, const string& type, int price);

	// Sort by name
	VO sortByName();

	// Sort by destination
	VO sortByDestination();

	// Sort by type then by price
	VO sortByTypePrice();

	// Filter offers with destination == given destination
	VO filterDestination(const string& destination);

	// Filter offers with prices less than the given price
	VO filterPrice(int price);

	// Returns an offer by its name
	const Offer searchOffer(const string& name);

	// Modifies an offer by its name
	void modifyOffer(const string& name, const string& newname, const string& newdestination, const string& newtype, int newtipe);

	// Changes the current repo
	void setAll(VO& newoffers);

	// Deletes an offer with a given name
	void deleteOfferName(const string& name);

	// Undo
	void undo(const string& command);
};
