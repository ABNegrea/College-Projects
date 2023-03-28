#pragma once
#include <vector>
#include <string>
#include <QtWidgets/QApplication>
#include <QLabel>
#include <QPushButton>
#include <QHBoxLayout>
#include <QFormLayout>
#include <QLineEdit>
#include <QTableWidget>
#include <QMessageBox>
#include <QHeaderView>
#include <QGroupBox>
#include <QRadioButton>
#include "ServiceOffer.h"
#include "WishList.h"
#include "Tests.h"
#include "WishList.h"
#include <qlistwidget.h>
using namespace std;

class OffersGUI : public QWidget
{
private:
	ServiceOffer& srv;
	WishList& wish;

	QListWidget* lstWish;

	QLabel* lblName = new QLabel{ "Offer name:" };
	QLabel* lblDestination = new QLabel{ "Offer destination:" };
	QLabel* lblType = new QLabel{ "Offer type:" };
	QLabel* lblPrice = new QLabel{ "Offer price:" };
	QLabel* lblNamewish = new QLabel{ "Offer name:" };
	QLabel* lblDestinationwish = new QLabel{ "Offer destination:" };
	QLabel* lblTypewish = new QLabel{ "Offer type:" };
	QLabel* lblPricewish = new QLabel{ "Offer price:" };
	QLabel* lblNameModify = new QLabel{ "New offer name:" };
	QLabel* lblDestinationModify = new QLabel{ "New offer destination:" };
	QLabel* lblTypeModify = new QLabel{ "New offer type:" };
	QLabel* lblPriceModify = new QLabel{ "New offer price:" };
	QLabel* lblNameToModify = new QLabel{ "Modify offer name:" };

	QLineEdit* editName;
	QLineEdit* editDestination;
	QLineEdit* editType;
	QLineEdit* editPrice;
	QLineEdit* editNamewish;
	QLineEdit* editDestinationwish;
	QLineEdit* editTypewish;
	QLineEdit* editPricewish;
	QLineEdit* editNameModify;
	QLineEdit* editDestinationModify;
	QLineEdit* editTypeModify;
	QLineEdit* editPriceModify;
	QLineEdit* editNameToModify;
	QLineEdit* editFilterDestination;
	QLineEdit* editFilterPrice;
	QLineEdit* editDeleteOffer;
	QLineEdit* editSearchOffer;
	QLineEdit* editRandomWish;
	QLineEdit* editExportWish;

	QPushButton* btnAddOffer;
	QPushButton* btnAddOfferwish;
	QPushButton* btnSortOffers;
	QPushButton* btnFilterDestination;
	QPushButton* btnFilterPrice;
	QPushButton* btnReloadData;
	QPushButton* btnReloadDataWish;
	QPushButton* btnDeleteOffer;
	QPushButton* btnSearchOffer;
	QPushButton* btnModifyOffer;
	QPushButton* btnUndoDelete;
	QPushButton* btnUndoAdd;
	QPushButton* btnUndoModify;
	QPushButton* btnWishList;
	QPushButton* btnAddRandomWish;
	QPushButton* btnEmptyWish;
	QPushButton* btnExportWish;

	QGroupBox* groupBoxSort = new QGroupBox(tr("Sort offer by"));

	QRadioButton* radioSrtTypePrice = new QRadioButton(QString::fromStdString("Type+Price"));
	QRadioButton* radioSrtDestination = new QRadioButton(QString::fromStdString("Destination"));

	QLabel* lblFilterCriteria = new QLabel{ "Filter option:" };

	QTableWidget* tableOffers;
	QTableWidget* tableWishlist;

	QListWidget* lstt;

	void initializeGUIComponents();
	void connectSignalsSlots();
	void reloadList(VO offers);
	void reloadWishlist();
public:
	OffersGUI(ServiceOffer& srv, WishList& wish) : srv{ srv }, wish{ wish }
	{
		initializeGUIComponents();
		connectSignalsSlots();
		reloadList(srv.getAll());
	}
	void guiAddOffer();
	void guiModifyOffer();
	void guiAddOfferWishlist();
	void guiEmptyWishList();
	void guiAddRandomWishList();
	void guiExportWishList();
};
