import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ApiService } from '../../services/api.service';

import Calendar from '../../types/calendar.type';
import { map } from 'rxjs';

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
	SelectedDay: number | null = null;
	SelectedSchedule: Date | null = null;

	Days: Map<Date, Date[]> = new Map();

	constructor(private apiService: ApiService) {
		const today: Date = new Date();
		this.Calendar = {
			Date: today,
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
		this.getCalendar(this.Calendar);
		setTimeout(() => {
			this.renderDays();
		}, 1); // 8-) attention dos d'ane
	}

	onDateSelected(day: number) {
		this.SelectedDay = day;
	}

	// API

	getCalendar(calendar: Calendar): void {
		this.apiService.getCalendar(calendar).subscribe(map	=> {
			this.Days = map;
		});
		console.log('Retour :');
		console.log(this.Days);
	}

	// Render

	renderDays() {
		let offset: number = this.Days.keys().next().value.getDay();
		if(offset == 0) {
			offset = 7;
		}

		//TODO: refaire tout dans une classe

		let daysGrid: HTMLElement | null = document.getElementById('daysgrid');
		console.log(daysGrid);
		if(daysGrid != null) {
			daysGrid.innerHTML = '<p>L</p><p>M</p><p>M</p><p>J</p><p>V</p><p>S</p><p>D</p>';
			for(let i = 0; i < Array.from(this.Days.keys()).length; i++) { //vérifier si on peut pas faire mieux
				let day: number = i+1;
				let dayButton: HTMLElement = document.createElement('button');
				dayButton.id = `day${day}`;
				if(dayButton.id == 'day1') {
					dayButton.style.gridColumnStart = `${offset}`;
				}
				dayButton.classList.add('day');
				if(Array.from(this.Days.entries()).at(i)?.[1].length === 0) {
					dayButton.classList.add('dayHasNoRoom');
				}
				dayButton.innerHTML = `${day}`;
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

	displayDate(day: number): string {
		const date: Date = Array.from(this.Days.keys()).at(day-1) as Date;
		return `${this.getDayName(date.getDay())} ${date.getDate()} ${this.getMonthName(date.getMonth())} ${date.getFullYear()}`;
	}
}
