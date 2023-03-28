#pragma once
#include "Offer.h"
#include "RepoOffer.h"
#include <vector>
using namespace std;
using VVO = vector<pair<VO,string>>;

class ActiuneUndo
{
public:
	VVO list;

	ActiuneUndo() = default;

	void virtual doUndo(RepoOffer& repo);

	virtual ~ActiuneUndo() = default;
};

class UndoAdauga : public ActiuneUndo
{
public:
	void doUndo(RepoOffer& repo) override;
};

class UndoSterge : public ActiuneUndo
{
public:
	void doUndo(RepoOffer& repo) override;
};

class UndoModifica : public ActiuneUndo
{
public:
	void doUndo(RepoOffer& repo) override;
};