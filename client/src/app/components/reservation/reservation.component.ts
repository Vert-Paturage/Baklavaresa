import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ApiService } from '../../services/api.service';

import Schedule from '../../types/schedule.type';
import Calendar from '../../types/calendar.type';
import Day from '../../types/day.type';

@Component({
	selector: 'app-reservation',
	standalone: true,
	imports: [FormsModule, ReactiveFormsModule, CommonModule],
	templateUrl: './reservation.component.html',
	styleUrls: ['./reservation.component.css'],
	providers: [ApiService]
})

export class ReservationComponent {

	// Variables

	MaxPeopleNumberPerReservation: number = 8;

	Calendar: Calendar;
	SelectedDay: Day | null = null;
	SelectedSchedule: Schedule | null = null;

	Days: Day[] = [];
	Schedules: Schedule[] = [];

	constructor(private apiService: ApiService) {
		const today: Date = new Date();
		this.Calendar = {
			month: today.getMonth(),
			year: today.getFullYear(),
			PeopleNumber: 0
		};
	}

	// On selection functions

	onNumberOfPersonChange(buttonValue: number) {
		if(buttonValue == this.Calendar.PeopleNumber) {
			return;
		}
		this.Calendar.PeopleNumber = buttonValue;
		this.SelectedDay = null;
		this.getDays(this.Calendar);
		this.renderDays();
	}

	onDateSelected(day: Day) {
		this.SelectedDay = day;
	}

	// API

	getDays(calendar: Calendar): void {
		this.Days = this.apiService.getDays(calendar);
	}

	// Render

	renderDays() {
		let offset: number = this.Days[0].index;
		if(offset == 0) {
			offset = 7;
		}

		let daysGrid: HTMLElement | null = document.getElementById('daysgrid');
		if(daysGrid != null) {
			daysGrid.innerHTML = '<p>L</p><p>M</p><p>M</p><p>J</p><p>V</p><p>S</p><p>D</p>';
			for(let i = 0; i < this.Days.length; i++) {
				let day: Day = this.Days[i];
				let dayButton: HTMLElement = document.createElement('button');
				dayButton.id = `day${day.day}`;
				if(dayButton.id == 'day1') {
					dayButton.style.gridColumnStart = `${offset}`;
				}
				dayButton.classList.add('day');
				if(!day.hasRoom) {
					dayButton.classList.add('dayHasNoRoom')
				}
				dayButton.innerHTML = `${day.day}`;
				dayButton.onclick = () => this.onDateSelected(day);
				daysGrid.appendChild(dayButton);
			}
		}
	}

	// Utils

	getMonthName(month: number) {
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

	getDayName(dayIndex: number) {
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

	displayDate(day: Day): string {
		return `${this.getDayName(day.index)} ${day.day} ${this.getMonthName(day.month)} ${day.year}`;
	}
}
