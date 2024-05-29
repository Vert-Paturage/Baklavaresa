type Day = {
	day: number;		// 1-31
	month: number;		// 0-11
	year: number;
	index: number;	//1=lundi 7=dimanche
	hasRoom: boolean;	// true si pour cette date il y a un horaire de disponible pour le nombre de personnes actuellement selectionné
};

export default Day;