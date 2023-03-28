#pragma once
#include "Offer.h"
#include <vector>
#include <string>
using namespace std;
using VS = vector<string>;

class ValidationException {
	VS errorMsg;
public:
	ValidationException() = default;
	ValidationException(VS errorMessages) :errorMsg{ errorMessages } {};
	string getErrorMsg();
};

class OfferValidator {
public:
	OfferValidator() = default;
	void validate(const Offer& off);
};