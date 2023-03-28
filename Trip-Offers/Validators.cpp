#include "Validators.h"

string ValidationException::getErrorMsg()
{
	string fullMsg = "";
	for (const string e : errorMsg)
	{
		fullMsg += e + '\n';
	}
	return fullMsg;
}

void  OfferValidator::validate(const Offer& off)
{
	VS errors;
	if (off.getDestination().length() == 0)
		errors.emplace_back("Invalid offer destination!");
	if (off.getType().length() == 0)
		errors.emplace_back("Invalid offer type!");
	if (off.getName().length() == 0)
		errors.emplace_back("Invalid offer name!");
	if (off.getPrice() < 0)
		errors.emplace_back("Invalid offer price!");
	if (errors.size() > 0)
		throw ValidationException(errors);
}