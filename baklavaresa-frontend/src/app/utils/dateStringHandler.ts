import { formatDate } from "@angular/common";

export function getMonthName(month: number) {
	const monthNames = [
		'Janvier',
		'Février',
		'Mars',
		'Avril',
		'Mai',
		'Juin',
		'Juillet',
		'Août',
		'Septembre',
		'Octobre',
		'Novembre',
		'Décembre'
	];

	return monthNames[month];
}

export function getDayName(dayIndex: number) {
	const dayNames = [
		'Dimanche',
		'Lundi',
		'Mardi',
		'Mercredi',
		'Jeudi',
		'Vendredi',
		'Samedi'
	];

	return dayNames[dayIndex];
}

export function getUTCISOString(date: Date): string {
	return formatDate(date, 'yyyy-MM-ddTHH:mm:ss', 'en-US');
}