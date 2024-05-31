import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ApiService } from '../../services/api.service';

import Calendar from '../../types/calendar.type';
import Day from '../../types/day.type';
import { firstValueFrom } from 'rxjs';

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

	Days: Day[] = [];

	constructor(private apiService: ApiService) {
		const today: Date = new Date();
		this.Calendar = {
			Date: today,
			PeopleNumber: 0
		};
	}

	// On selection functions

	async onNumberOfPersonChange(buttonValue: number) {
		if(buttonValue == this.Calendar.PeopleNumber) {
			return;
		}
		this.Calendar.PeopleNumber = buttonValue;
		this.SelectedDay = null;
		await this.getCalendar(this.Calendar);
		this.renderDays();
	}

	onDateSelected(day: number) {
		this.SelectedDay = day;
		console.log(this.Days[day-1].day);
	}

	// API

	async getCalendar(calendar: Calendar): Promise<void> {
		this.Days = await firstValueFrom(this.apiService.getCalendar(calendar));
	}

	// Render

	renderDays() {
		let offset: number = this.Days[0].day.getDay();
		if(offset == 0) {
			offset = 7;
		}

		//TODO: refaire tout dans une classe

		let daysGrid: HTMLElement | null = document.getElementById('daysgrid');
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
				if(this.Days[i].slots.length === 0) {
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
		const date: Date = this.Days[day-1].day;
		return `${this.getDayName(date.getDay())} ${date.getDate()} ${this.getMonthName(date.getMonth())} ${date.getFullYear()}`;
	}
}
