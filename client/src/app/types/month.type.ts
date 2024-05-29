import Day from './day.type';

type Month = {
	month: number;				// 0-11
	year: number;
	numberOfDays: number;		// pour afficahge calendrier
	days: Day[];				// liste des jours du mois
};

export default Month;