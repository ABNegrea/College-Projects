/********************************************************************************
** Form generated from reading UI file 'Laborator10OOP.ui'
**
** Created by: Qt User Interface Compiler version 6.3.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_LABORATOR10OOP_H
#define UI_LABORATOR10OOP_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_Laborator10OOPClass
{
public:
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QWidget *centralWidget;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *Laborator10OOPClass)
    {
        if (Laborator10OOPClass->objectName().isEmpty())
            Laborator10OOPClass->setObjectName(QString::fromUtf8("Laborator10OOPClass"));
        Laborator10OOPClass->resize(600, 400);
        menuBar = new QMenuBar(Laborator10OOPClass);
        menuBar->setObjectName(QString::fromUtf8("menuBar"));
        Laborator10OOPClass->setMenuBar(menuBar);
        mainToolBar = new QToolBar(Laborator10OOPClass);
        mainToolBar->setObjectName(QString::fromUtf8("mainToolBar"));
        Laborator10OOPClass->addToolBar(mainToolBar);
        centralWidget = new QWidget(Laborator10OOPClass);
        centralWidget->setObjectName(QString::fromUtf8("centralWidget"));
        Laborator10OOPClass->setCentralWidget(centralWidget);
        statusBar = new QStatusBar(Laborator10OOPClass);
        statusBar->setObjectName(QString::fromUtf8("statusBar"));
        Laborator10OOPClass->setStatusBar(statusBar);

        retranslateUi(Laborator10OOPClass);

        QMetaObject::connectSlotsByName(Laborator10OOPClass);
    } // setupUi

    void retranslateUi(QMainWindow *Laborator10OOPClass)
    {
        Laborator10OOPClass->setWindowTitle(QCoreApplication::translate("Laborator10OOPClass", "Laborator10OOP", nullptr));
    } // retranslateUi

};

namespace Ui {
    class Laborator10OOPClass: public Ui_Laborator10OOPClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_LABORATOR10OOP_H
