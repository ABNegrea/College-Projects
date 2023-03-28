// MAIN
#include <QtWidgets/QApplication>
#include "ServiceOffer.h"
#include "WishList.h"
#include "Tests.h"
#include "ConsoleUI.h"
#include "OffersGUI.h"
#include <crtdbg.h>

int Console()
{
	{
		RepoOffer repo;
		//RepoOfferFile repoFile{ "offer.txt" };
		ActiuneUndo undoList;
		OfferValidator val;
		ServiceOffer srv{ repo, undoList, val };
		WishList wish;
		ConsoleUI ui{ srv,wish };
		testAll();
		ui.start();
	}
	_CrtDumpMemoryLeaks();
	return 0;
}

int main(int argc, char* argv[])
{
	testAll();
	QApplication a(argc, argv);
	//RepoOffer repo;
	RepoOfferFile repoFile{ "offers.txt" };
	ActiuneUndo undo;
	OfferValidator val;
	//WishList wish;
	WishListFile wish{ "wishlist.txt" };
	ServiceOffer srv{ repoFile, undo, val };
	OffersGUI gui{ srv,wish };
	gui.show();
	a.exec();
	return 0;
}
