#include "ActiuneUndo.h"
using namespace std;

void ActiuneUndo::doUndo(RepoOffer& repo)
{
	repo.setAll(list.back().first);
	list.pop_back();
}

void UndoAdauga::doUndo(RepoOffer& repo)
{
	for (size_t i = list.size() - 1; i >= 0 && i < list.size(); i--)
		if (list[i].second == "adauga")
		{
			repo.setAll(list[i].first);
			list.erase(list.begin() + i);
			i = 2000000005;
		}
}

void UndoSterge::doUndo(RepoOffer& repo)
{
	for (size_t i = list.size() - 1; i >= 0 && i < list.size(); i--)
		if (list[i].second == "sterge")
		{
			repo.setAll(list[i].first);
			list.erase(list.begin() + i);
			i = 2000000005;
		}
}

void UndoModifica::doUndo(RepoOffer& repo)
{
	for (size_t i = list.size() - 1; i >= 0 && i < list.size(); i--)
		if (list[i].second == "modifica" && i)
		{
			repo.setAll(list[i].first);
			list.erase(list.begin() + i);
			i = 2000000005;
		}
}