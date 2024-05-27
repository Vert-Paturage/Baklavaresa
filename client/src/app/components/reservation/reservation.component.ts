import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ApiService } from '../../services/api.service';
import Schedule from '../../types/schedule.type';

@Component({
	selector: 'app-reservation',
	standalone: true,
	imports: [FormsModule, ReactiveFormsModule, CommonModule],
	templateUrl: './reservation.component.html',
	styleUrls: ['./reservation.component.css'], // Correction de styleUrl en styleUrls
	providers: [ApiService]
})
export class ReservationComponent {
	maxPeopleNumberPerReservation: number = 8;

	peopleNumberSelected: number = 2;

	displayedMonth: Month | null = null;
	selectedDate: Date | null = null;

	availableSchedules: Schedule[] = [];
	selectedSchedule: Schedule | null = null;

	constructor(private apiService: ApiService) {
		const today = new Date();

		this.displayedMonth = {
			name: '',
			month: today.getMonth(),
			year: today.getFullYear(),
			firstDay: 0,
			numberOfDays: 0,
			dates: []
		};
	}

	onNumberOfPersonChange(buttonValue: number) {
		console.log(buttonValue);
		this.peopleNumberSelected = buttonValue;

		this.updateDisplayedMonth();
	}

	updateDisplayedMonth() {
		//appel api
	}

	onDateSelected(date: Date) {
		this.selectedDate = date;

		this.updateDisplayedSchedules();
	}

	updateDisplayedSchedules() {
		//appel api
	}

	selectSchedule(schedule: Schedule) {
		this.selectedSchedule = schedule;
	}
}

type Month = {
	name: string;
	month: number;
	year: number;
	firstDay: number;
	numberOfDays: number;

	dates: Date[];
};

type Date = {
	day: number;
	month: number;
	year: number;

	hasRoom: boolean;
};
