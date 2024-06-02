# Notes

## Echange de données - POV frontend

-> Au début on a juste le prompt du nombre de personnes avec aucun valeur par défaut
ensuite on choisit un nb de personne -> requete GET.

Requete :

Send --> le mois, l'année et un nombre de personnes
Receive --> la liste des jours de ce mois et pour chaque jour un booleen true si il y a au moins 1 creneau ce jour la pour le nb de personnes specifié, false sinon

```ts
type Calendar = {
	Month: number;
	Year: number;
	NumberOfPeople: number;
}

type Day = {
	Number: number;
	Index: number;

	hasRoom: boolean;
}

class Test {
	GetDayListFromMonthAndNumberOfPeople(calendar: Calendar): Day[] {
		return [];
	}
}
```
