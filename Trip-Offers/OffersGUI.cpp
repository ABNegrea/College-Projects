#include "OffersGUI.h"

void OffersGUI::initializeGUIComponents()
{
	QHBoxLayout* lyMain = new QHBoxLayout;
	this->setLayout(lyMain);

	QWidget* left = new QWidget;
	QVBoxLayout* lyLeft = new QVBoxLayout;
	left->setLayout(lyLeft);

	QWidget* form = new QWidget;
	QFormLayout* lyForm = new QFormLayout;
	form->setLayout(lyForm);
	editName = new QLineEdit;
	editDestination = new QLineEdit;
	editType = new QLineEdit;
	editPrice = new QLineEdit;
	lyForm->addRow(lblName, editName);
	lyForm->addRow(lblDestination, editDestination);
	lyForm->addRow(lblType, editType);
	lyForm->addRow(lblPrice, editPrice);
	btnAddOffer = new QPushButton("Add offer");
	btnUndoAdd = new QPushButton("Undo Add");
	lyForm->addRow(btnUndoAdd, btnAddOffer);
	lyLeft->addWidget(form);

	QWidget* formModify = new QWidget;
	QFormLayout* lyFormModify = new QFormLayout;
	formModify->setLayout(lyFormModify);
	editNameModify = new QLineEdit;
	editNameToModify = new QLineEdit;
	editDestinationModify = new QLineEdit;
	editTypeModify = new QLineEdit;
	editPriceModify = new QLineEdit;
	lyFormModify->addRow(lblNameToModify, editNameToModify);
	lyFormModify->addRow(lblNameModify, editNameModify);
	lyFormModify->addRow(lblDestinationModify, editDestinationModify);
	lyFormModify->addRow(lblTypeModify, editTypeModify);
	lyFormModify->addRow(lblPriceModify, editPriceModify);
	btnModifyOffer = new QPushButton("ModifyOffer");
	btnUndoModify = new QPushButton("Undo Modify");
	lyFormModify->addRow(btnUndoModify, btnModifyOffer);
	lyLeft->addWidget(formModify);

	QWidget* middle = new QWidget;
	QVBoxLayout* lyMiddle = new QVBoxLayout;
	middle->setLayout(lyMiddle);

	QWidget* formSearch = new QWidget;
	QFormLayout* lyFormSearch = new QFormLayout;
	formSearch->setLayout(lyFormSearch);
	editSearchOffer = new QLineEdit();
	btnSearchOffer = new QPushButton("Search offer");
	lyFormSearch->addRow(editSearchOffer, btnSearchOffer);
	lyMiddle->addWidget(formSearch);

	QWidget* formDelete = new QWidget;
	QFormLayout* lyFormDelete = new QFormLayout;
	formDelete->setLayout(lyFormDelete);
	editDeleteOffer = new QLineEdit();
	btnDeleteOffer = new QPushButton("Delete offer");
	btnUndoDelete = new QPushButton("Undo delete");
	lyFormDelete->addRow(editDeleteOffer, btnDeleteOffer);
	lyFormDelete->addRow(btnUndoDelete);
	lyMiddle->addWidget(formDelete);

	QWidget* formFilter = new QWidget;
	QFormLayout* lyFormFilter = new QFormLayout;
	formFilter->setLayout(lyFormFilter);
	editFilterDestination = new QLineEdit();
	editFilterPrice = new QLineEdit();
	btnFilterDestination = new QPushButton("Filter offers by destination");
	btnFilterPrice = new QPushButton("Filter offers by price");
	lyFormFilter->addRow(editFilterPrice, btnFilterPrice);
	lyFormFilter->addRow(editFilterDestination, btnFilterDestination);
	lyMiddle->addWidget(formFilter);

	QVBoxLayout* lyRadioBox = new QVBoxLayout;
	this->groupBoxSort->setLayout(lyRadioBox);
	lyRadioBox->addWidget(radioSrtTypePrice);
	lyRadioBox->addWidget(radioSrtDestination);
	btnSortOffers = new QPushButton("Sort offers");
	lyRadioBox->addWidget(btnSortOffers);
	lyMiddle->addWidget(groupBoxSort);

	btnWishList = new QPushButton("Go to your wishlist");
	lyMiddle->addWidget(btnWishList);

	QWidget* right = new QWidget;
	QVBoxLayout* lyRight = new QVBoxLayout;
	right->setLayout(lyRight);
	int noLines = 10;
	int noColumns = 4;
	this->tableOffers = new QTableWidget{ noLines, noColumns };
	QStringList tblHeaderList;
	tblHeaderList << "Name" << "Destination" << "Type" << "Price";
	this->tableOffers->setHorizontalHeaderLabels(tblHeaderList);
	this->tableOffers->horizontalHeader()->setSectionResizeMode(QHeaderView::ResizeToContents);
	lyRight->addWidget(tableOffers);

	btnReloadData = new QPushButton("Reload data");
	lyRight->addWidget(btnReloadData);

	lyMain->addWidget(left);
	lyMain->addWidget(middle);
	lyMain->addWidget(right);
}

void OffersGUI::reloadList(VO offers)
{
	this->tableOffers->clearContents();
	this->tableOffers->setRowCount(offers.size());

	int lineNumber = 0;
	for (auto& offer : offers) {
		this->tableOffers->setItem(lineNumber, 0, new QTableWidgetItem(QString::fromStdString(offer.getName())));
		this->tableOffers->setItem(lineNumber, 1, new QTableWidgetItem(QString::fromStdString(offer.getDestination())));
		this->tableOffers->setItem(lineNumber, 2, new QTableWidgetItem(QString::fromStdString(offer.getType())));
		this->tableOffers->setItem(lineNumber, 3, new QTableWidgetItem(QString::number(offer.getPrice())));
		lineNumber++;
	}
}

void OffersGUI::reloadWishlist()
{
		lstWish->clear();
		for (const auto& wish : wish.getAll())
		{
			QListWidgetItem* item = new QListWidgetItem(QString::fromStdString(wish.getName() + " | " + wish.getDestination() + " | " + wish.getType() + " | " + to_string(wish.getPrice())));
			item->setData(Qt::UserRole, QString::fromStdString(wish.getName() + " " + wish.getDestination() + " " + wish.getType() + " " + to_string(wish.getPrice())));
			lstWish->addItem(item);
		}
}

void OffersGUI::connectSignalsSlots()
{
	QObject::connect(btnAddOffer, &QPushButton::clicked, this, &OffersGUI::guiAddOffer);

	QObject::connect(btnModifyOffer, &QPushButton::clicked, this, &OffersGUI::guiModifyOffer);

	QObject::connect(btnSortOffers, &QPushButton::clicked, [&]() {
		QMessageBox msgBox;
		msgBox.setText("The document has been modified.");
		msgBox.exec();
		if (this->radioSrtTypePrice->isChecked())
			this->reloadList(srv.sortByTypePrice());
		else if (this->radioSrtDestination->isChecked())
			this->reloadList(srv.sortByDestination());
		});

	QObject::connect(btnFilterDestination, &QPushButton::clicked, [&]() {
		string filterC = this->editFilterDestination->text().toStdString();
		this->reloadList(srv.filterDestination(filterC));
		});

	QObject::connect(btnDeleteOffer, &QPushButton::clicked, [&]() {
		string filterC = this->editDeleteOffer->text().toStdString();
		srv.deleteOfferName(filterC);
		this->reloadList(srv.getAll());
		});

	QObject::connect(btnFilterPrice, &QPushButton::clicked, [&]() {
		int filterC = this->editFilterPrice->text().toInt();
		this->reloadList(srv.filterPrice(filterC));
		});

	QObject::connect(btnSearchOffer, &QPushButton::clicked, [&]() {
		string filterC = this->editSearchOffer->text().toStdString();
		VO of;
		of.push_back(srv.searchOffer(filterC));
		this->reloadList(of);
		});

	QObject::connect(btnReloadData, &QPushButton::clicked, [&]() {
		this->reloadList(srv.getAll());
		});

	QObject::connect(btnUndoDelete, &QPushButton::clicked, [&]() {
		srv.undo("sterge");
		this->reloadList(srv.getAll());
		});

	QObject::connect(btnUndoModify, &QPushButton::clicked, [&]() {
		srv.undo("modifica");
		this->reloadList(srv.getAll());
		});

	QObject::connect(btnUndoAdd, &QPushButton::clicked, [&]() {
		srv.undo("adauga");
		this->reloadList(srv.getAll());
		});

	QObject::connect(btnWishList, &QPushButton::clicked, [&]()
		{
			QWidget* wishList = new QWidget;
			QFormLayout* lywishlist = new QFormLayout;
			wishList->setLayout(lywishlist);

			QWidget* leftwish = new QWidget;
			QVBoxLayout* lyLeftwish = new QVBoxLayout;
			leftwish->setLayout(lyLeftwish);

			QWidget* formwish = new QWidget;
			QFormLayout* lyFormwish = new QFormLayout;
			formwish->setLayout(lyFormwish);
			editNamewish = new QLineEdit;
			editDestinationwish = new QLineEdit;
			editTypewish = new QLineEdit;
			editPricewish = new QLineEdit;
			lyFormwish->addRow(lblNamewish, editNamewish);
			lyFormwish->addRow(lblDestinationwish, editDestinationwish);
			lyFormwish->addRow(lblTypewish, editTypewish);
			lyFormwish->addRow(lblPricewish, editPricewish);
			btnAddOfferwish = new QPushButton("Add offer to wishlist");
			lyFormwish->addRow(btnAddOfferwish);
			lyLeftwish->addWidget(formwish);


			QWidget* formwish2 = new QWidget;
			QFormLayout* lyFormwish2 = new QFormLayout;
			formwish2->setLayout(lyFormwish2);
			editRandomWish = new QLineEdit;
			btnAddRandomWish = new QPushButton("Add random offers");
			lyFormwish2->addRow(editRandomWish,btnAddRandomWish);
			btnEmptyWish = new QPushButton("Empty Wishlist");
			lyFormwish2->addRow(btnEmptyWish);

			btnExportWish = new QPushButton("Export to file");
			editExportWish = new QLineEdit;
			lyFormwish2->addRow(editExportWish, btnExportWish);

			lyLeftwish->addWidget(formwish2);

			QWidget* rightwish = new QWidget;
			QVBoxLayout* lyRightwish = new QVBoxLayout;
			rightwish->setLayout(lyRightwish);
			lstWish = new QListWidget;

			lyRightwish->addWidget(lstWish);

			btnReloadDataWish = new QPushButton("Reload data");
			lyRightwish->addWidget(btnReloadDataWish);

			QObject::connect(btnAddOfferwish, &QPushButton::clicked, this, &OffersGUI::guiAddOfferWishlist);
			QObject::connect(btnReloadDataWish, &QPushButton::clicked, this, &OffersGUI::reloadWishlist);
			QObject::connect(btnEmptyWish, &QPushButton::clicked, this, &OffersGUI::guiEmptyWishList);
			QObject::connect(btnAddRandomWish, &QPushButton::clicked, this, &OffersGUI::guiAddRandomWishList);
			QObject::connect(btnExportWish, &QPushButton::clicked, this, &OffersGUI::guiExportWishList);

			lywishlist->addWidget(leftwish);
			lywishlist->addWidget(rightwish);

			wishList->show();
		});
}

void OffersGUI::guiAddOffer()
{
	string name = editName->text().toStdString();
	string destination = editDestination->text().toStdString();
	string type = editType->text().toStdString();
	int price = editPrice->text().toInt();

	editName->clear();
	editDestination->clear();
	editType->clear();
	editPrice->clear();

	this->srv.addOffer(name, destination, type, price);
	this->reloadList(this->srv.getAll());

	QMessageBox::information(this, "Info", QString::fromStdString("Offer added successfully."));
}

void OffersGUI::guiAddOfferWishlist()
{
	string name = editNamewish->text().toStdString();
	string destination = editDestinationwish->text().toStdString();
	string type = editTypewish->text().toStdString();
	int price = editPricewish->text().toInt();

	editNamewish->clear();
	editDestinationwish->clear();
	editTypewish->clear();
	editPricewish->clear();

	Offer of{ name,destination,type,price };
	wish.addOfferWishList(of);
	this->reloadWishlist();
}

void OffersGUI::guiModifyOffer()
{
	string namec = editNameToModify->text().toStdString();
	string name = editNameModify->text().toStdString();
	string destination = editDestinationModify->text().toStdString();
	string type = editTypeModify->text().toStdString();
	int price = editPriceModify->text().toInt();

	editNameToModify->clear();
	editNameModify->clear();
	editDestinationModify->clear();
	editTypeModify->clear();
	editPriceModify->clear();

	this->srv.modifyOffer(namec, name, destination, type, price);
	this->reloadList(this->srv.getAll());
}

void OffersGUI::guiEmptyWishList()
{
	wish.emptyWishList();
	reloadWishlist();
}

void OffersGUI::guiAddRandomWishList()
{
	int nr = editRandomWish->text().toInt();
	wish.addRandomOffers(srv.getAll(),nr);
	reloadWishlist();
}

void OffersGUI::guiExportWishList()
{
	string filename = editExportWish->text().toStdString();
	wish.exportFile(filename);
}