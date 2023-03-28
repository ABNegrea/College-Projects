#include "ConsoleUI.h"
#include <iostream>

void ConsoleUI::showAllUI(const VO& offers) 
{
	cout << "\nOffers:\n";
	for (const auto& offer : offers)
	{
		showOneUI(offer);
	}
	cout << "---------------------------------------------------------------------------\n\n";
}

void ConsoleUI::showOneUI(const Offer offer)
{
	cout << "Name: " << offer.getName() << " | Destination: " << offer.getDestination();
	cout << " | Type: " << offer.getType() << " | Price: " << offer.getPrice() << '\n';
}

void ConsoleUI::addUI() 
{
	string type, destination, name;
	int price;
	cout << "Input name: ";
	cin >> name;
	cout << "Input destination: ";
	cin >> destination;
	cout << "Input type: ";
	cin >> type;
	cout << "Input price: ";
	cin >> price;
	try
	{
		srv.addOffer(name, destination, type, price);
		cout << "Offer added successfully\n\n";
	}
	catch (ValidationException& ex)
	{
		cout << ex.getErrorMsg();
	}
}

void ConsoleUI::start()
{
	while (true)
	{
		cout << "Commands:\n1. Add offer\n2. Show offers\n3. Sort by name\n4. Sort by destination\n5. Sort by type+price\n6. Filter by destination";
		cout<<"\n7. Filter by price\n8. Search offer\n9. Modify offer\n10. Delete offer\n11. Undo\n12. WishList\n0. Exit\n";
		int cmd;
		cout << "Your command: ";
		cin >> cmd;
		cout << '\n';
		if (cmd == 1)
			addUI();
		else if (cmd == 2)
			showAllUI(srv.getAll());
		else if (cmd == 3)
			showAllUI(srv.sortByName());
		else if (cmd == 4)
			showAllUI(srv.sortByDestination());
		else if (cmd == 5)
			showAllUI(srv.sortByTypePrice());
		else if (cmd == 6)
		{
			string destination;
			cout << "Input destination: ";
			cin >> destination;
			try
			{
				showAllUI(srv.filterDestination(destination));
			}
			catch (ValidationException& ex)
			{
				cout << ex.getErrorMsg();
			}
		}
		else if (cmd == 7)
		{
			int price;
			cout << "Input price: ";
			cin >> price;
			try
			{
				showAllUI(srv.filterPrice(price));
			}
			catch (ValidationException& ex)
			{
				cout << ex.getErrorMsg();
			}
		}
		else if (cmd == 8)
		{
			string name;
			cout << "Input name: ";
			cin >> name;
			try
			{
				showOneUI(srv.searchOffer(name));
			}
			catch (ValidationException& ex)
			{
				cout << ex.getErrorMsg();
			}
		}
		else if (cmd == 9)
		{
			string name, newname, newtype, newdestination;
			int newprice;
			cout << "Input name: ";
			cin >> name;
			cout << "Input new name: ";
			cin >> newname;
			cout << "Input new destination: ";
			cin >> newdestination;
			cout << "Input new type: ";
			cin >> newtype;
			cout << "Input new price: ";
			cin >> newprice;
			try
			{
				srv.modifyOffer(name, newname, newdestination, newtype, newprice);
				showAllUI(srv.getAll());
			}
			catch (ValidationException& ex)
			{
				cout << ex.getErrorMsg();
			}
		}
		else if (cmd == 10)
		{
			string name;
			cout << "Input name: ";
			cin >> name;
			try
			{
				srv.deleteOfferName(name);
				showAllUI(srv.getAll());
			}
			catch (ValidationException& ex)
			{
				cout << ex.getErrorMsg();
			}
		}
		else if (cmd == 11)
		{
			int undocmd;
			cout << "1. Undo Add\n2. Undo Delete\n3. Undo Modify\nYour command: ";
			cin >> undocmd;
			if (undocmd == 1)
				srv.undo("adauga");
			else if (undocmd == 2)
				srv.undo("sterge");
			else if (undocmd == 3)
				srv.undo("modifica");
			else
				cout << "Invalid input!\n";
			cout << '\n';
		}
		else if (cmd == 12)
			startwish();
		else if (cmd == 0)
			break;
		else
			cout << "Invalid command!\n\n";
	}
}

void ConsoleUI::addUIWish()
{
	string type, destination, name;
	int price;
	cout << "Input name: ";
	cin >> name;
	cout << "Input destination: ";
	cin >> destination;
	cout << "Input type: ";
	cin >> type;
	cout << "Input price: ";
	cin >> price;
	Offer of1(name, destination, type, price);
	try
	{
		wish.addOfferWishList(of1);
		cout << "Offer added successfully\n\n";
	}
	catch (ValidationException& ex)
	{
		cout << ex.getErrorMsg();
	}
}

void ConsoleUI::startwish()
{
	while (true)
	{
		cout << "\nWishList commands:\n1. Add offer to wishlist\n2. Empty wishlist\n3. Generate random offers\n4. Show the wishlist\n";
		cout << "5. Save to file.\n0. Go back to main menu\n";
		int cmd;
		cout << "Your command: ";
		cin >> cmd;
		if (cmd == 1)
			addUIWish();
		else if (cmd == 2)
			wish.emptyWishList();
		else if (cmd == 3)
		{
			int nr;
			cout << "Number of offers to add: ";
			cin >> nr;
			try
			{
				wish.addRandomOffers(srv.getAll(), nr);
			}
			catch (ValidationException& ex)
			{
				cout << ex.getErrorMsg();
			}
			wish.addRandomOffers(srv.getAll(),nr);
		}
		else if (cmd == 4)
		{
			showAllUI(wish.getAll());
		}
		else if (cmd == 5)
		{
			string file;
			cout << "\nThe name of the file you want to export to (with extension .html or .csv): ";
			cin >> file;
			wish.exportFile(file);
			cout << "The file was succesfully saved.\n";
		}
		else if (cmd == 0)
		{
			cout << "\n\n";
			break;
		}
		else
			cout << "Invalid command!\n\n";
	}
}