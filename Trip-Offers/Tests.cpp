#include "Tests.h"

void testAll()
{
	testOffer();
	testRepo();
	testService();
	testValidators();
	testWishList();
	testUndo();
}

void testOffer()
{
	Offer of1 = Offer("OfertaToamna", "Hawaii", "All-Inclusive", 2999);
	assert(of1.getName() == "OfertaToamna");
	assert(of1.getDestination() == "Hawaii");
	assert(of1.getPrice() == 2999);
	assert(of1.getType() == "All-Inclusive");

	of1.setName("OfertaPrimavara");
	of1.setDestination("Madrid");
	of1.setPrice(2998);
	of1.setType("Basic");
	assert(of1.getName() == "OfertaPrimavara");
	assert(of1.getDestination() == "Madrid");
	assert(of1.getPrice() == 2998);
	assert(of1.getType() == "Basic");

	Offer of2 = Offer("OfertaIarna", "Bahamas", "Diner+Breakfast", 2140);

	assert(cmpDestination(of1, of2) == false);
	assert(cmpName(of1, of2) == false);
	assert(cmpTypePrice(of1, of2) == true);

	of2.setType("Basic");
	assert(cmpTypePrice(of1, of2) == false);
}

void testRepo()
{
	RepoOffer repo;
	Offer of1 = Offer("OfertaToamna", "Hawaii", "All-Inclusive", 2999);

	repo.store(of1);
	assert(repo.getAll().size() == 1);
	
	Offer aux = repo.find("OfertaToamna");
	Offer of2 = Offer("OfertaIarna", "Bahamas", "Diner+Breakfast", 2140);
	assert(aux.getDestination()=="Hawaii");
	assert(repo.exist(aux) == true);
	assert(repo.exist(of2) == false);
	assert(aux.getDestination() == "Hawaii");
	of1 = Offer("OfertaMai", "Hawaii", "All-Inclusive", 2999);
	repo.store(of1);
	repo.deleteOffer(1);
	assert(repo.getAll().size() == 1);
	VO repo2;
	of1 = Offer("OfertaExclusiva", "Hawaii", "All-Inclusive", 2999);
	repo2.emplace_back(of1);
	of1 = Offer("OfertaExclusiva", "Hawaii", "All-Inclusive", 2999);
	repo2.emplace_back(of1);
	repo.setAll(repo2);
	assert(repo.getAll().size() == 2);
	assert(repo.getAll()[0].getName() == "OfertaExclusiva");
	assert(repo.getAll()[1].getName() == "OfertaExclusiva");
}

void testService()
{
	RepoOffer repo;
	ActiuneUndo undoList;
	OfferValidator val;
	ServiceOffer srv{ repo, undoList,val };
	srv.addOffer("OfertaToamna", "Hawaii", "All-Inclusive", 2999);
	srv.addOffer("OfertaIarna", "Bahamas", "Diner+Breakfast", 2140);
	assert(srv.getAll().size() == 2);
	srv.sortByDestination();
	assert(srv.getAll()[0].getDestination() == "Hawaii");
	srv.sortByName();
	assert(srv.getAll()[0].getName() == "OfertaToamna");
	srv.sortByTypePrice();
	assert(srv.getAll()[0].getPrice() == 2999);
	VO aux1 = srv.filterDestination("Hawaii");
	assert(aux1.size() == 1);
	aux1 = srv.filterPrice(1);
	assert(aux1.size() == 0);
	aux1 = srv.filterPrice(199999);
	assert(aux1.size() == 2);

	VO repo2;
	Offer of1 = Offer("OfertaExclusiva", "Hawaii", "All-Inclusive", 2999);
	repo2.emplace_back(of1);
	of1 = Offer("OfertaExclusiva2", "Hawaii", "All-Inclusive", 2999);
	repo2.emplace_back(of1);
	repo.setAll(repo2);
	srv.setAll(repo2);
	assert(srv.getAll().size() == 2);
	assert(srv.getAll()[0].getName() == "OfertaExclusiva");
	assert(srv.getAll()[1].getName() == "OfertaExclusiva2");
	srv.modifyOffer("OfertaExclusiva", "test", "test", "test", 123);
	assert(srv.getAll()[0].getName() == "test");
	of1 = srv.searchOffer("test");
	assert(of1.getName() == "test");
	srv.deleteOfferName("test");
	assert(srv.getAll().size() == 1);
}

void testWishList()
{
	WishList test;
	Offer of1 = Offer("OfertaExclusiva", "Hawaii", "All-Inclusive", 2999);
	assert(test.getAll().size() == 0);
	test.addOfferWishList(of1);
	assert(test.getAll().size() == 1);
	test.emptyWishList();
	assert(test.getAll().size() == 0);
	VO add;
	add.emplace_back(of1);
	add.emplace_back(of1);
	add.emplace_back(of1);
	add.emplace_back(of1);
	add.emplace_back(of1);
	test.addRandomOffers(add, 5);
	assert(test.getAll().size() == 5);
	test.exportFile("tedfdsfdsfsdfst.html");
}

void testValidators()
{
	RepoOffer of;
	ActiuneUndo undoList;
	OfferValidator val;
	ServiceOffer srv{ of, undoList, val };
	try
	{
		srv.addOffer("", "Hawaii", "All-Inclusive", 2999);
	}
	catch (ValidationException& e)
	{
		assert(true);
		assert(e.getErrorMsg() == "Invalid offer name!\n");
	}
	try
	{
		srv.addOffer("ssss", "", "All-Inclusive", 2999);
	}
	catch (ValidationException& e)
	{
		assert(true);
		assert(e.getErrorMsg() == "Invalid offer destination!\n");
	}
	try
	{
		srv.addOffer("sssss", "Hawaii", "", 2999);
	}
	catch (ValidationException& e)
	{
		assert(true);
		assert(e.getErrorMsg() == "Invalid offer type!\n");
	}
	try
	{
		srv.addOffer("ssss", "Hawaii", "All-Inclusive", -3);
	}
	catch (ValidationException& e)
	{
		assert(true);
		assert(e.getErrorMsg() == "Invalid offer price!\n");
	}
}

void testUndo()
{
	RepoOffer of;
	ActiuneUndo undoList;
	OfferValidator val;
	ServiceOffer srv{ of, undoList, val };
	Offer of1 = Offer("OfertaExclusiva", "Hawaii", "All-Inclusive", 2999);
	srv.addOffer("OfertaExclusiva", "Hawaii", "All-Inclusive", 2999);
	srv.addOffer("OfertaExclusiva", "Hawaii", "All-Inclusive", 2999);
	srv.modifyOffer("OfertaExclusiva", "test", "test", "test", 123);
	assert(srv.getAll()[1].getName() == "test");
	srv.undo("modifica");
	assert(srv.getAll()[1].getName() == "OfertaExclusiva");
	srv.undo("adauga");
	assert(srv.getAll().size() == 1);
	srv.deleteOfferName("OfertaExclusiva");
	assert(srv.getAll().size() == 0);
	srv.undo("sterge");
	assert(srv.getAll().size() == 1);
	undoList.doUndo(of);
}