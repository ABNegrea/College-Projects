#pragma once
#include "ServiceOffer.h"
#include "WishList.h"
#include "Validators.h"

class ConsoleUI {
	ServiceOffer& srv;
	WishList& wish;

	// Reads an offer
	void addUI();

	void addUIWish();

	// Prints a list of offers
	void showAllUI(const VO& alloffers);

	// Prints an offer
	void showOneUI(const Offer offer);

public:

	ConsoleUI(ServiceOffer& srv, WishList& wish) noexcept :srv{ srv }, wish{ wish } {}

	//Doesn't allow duplicates
	ConsoleUI(const ConsoleUI& ot) = delete;

	void start();

	void startwish();
};