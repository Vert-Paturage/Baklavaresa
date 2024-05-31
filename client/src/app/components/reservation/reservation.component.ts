import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';

import { CalendarComponent } from '../calendar/calendar.component';
import { ScheduleSelectorComponent } from '../scheduleselector/scheduleselector.component';

import { ApiService } from '../../services/api.service';

import Calendar from '../../types/calendar.type';
import Day from '../../types/day.type';

@Component({
	selector: 'app-reservation',
	standalone: true,
	imports: [FormsModule, ReactiveFormsModule, CommonModule, CalendarComponent, ScheduleSelectorComponent],
	templateUrl: './reservation.component.html',
	styleUrls: ['./reservation.component.css'],
	providers: [ApiService]
})

export class ReservationComponent {

	// Variables

	MaxPeopleNumberPerReservation: number = 8;

	Calendar: Calendar;
	SelectedDay: Day | null = null;
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
	}

	onDateSelected(day: Day) {
		this.SelectedDay = day;
	}

	onScheduleSelected(schedule: Date) {
		this.SelectedSchedule = schedule;
	}

	// API

	async getCalendar(calendar: Calendar): Promise<void> {
		this.Days = await firstValueFrom(this.apiService.getCalendar(calendar));
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

	displayDate(day: Date): string {
		return `${this.getDayName(day.getDay())} ${day.getDate()} ${this.getMonthName(day.getMonth())} ${day.getFullYear()}`;
	}

	getReservationCurrentStateMessage(): string {
		var result: string = "";
		if(this.Calendar.PeopleNumber != 0)
		{
			result += `Réservation pour ${this.Calendar.PeopleNumber} personne${this.Calendar.PeopleNumber === 1 ? '' : 's'}`;
			if(this.SelectedDay != null)
			{
				result += ` le ${this.displayDate(this.SelectedDay.day)}`;
				if(this.SelectedSchedule != null)
				{
					result += ` à ${this.SelectedSchedule.getHours()}:${this.SelectedSchedule.getMinutes() < 10 ? '0' + this.SelectedSchedule.getMinutes() : this.SelectedSchedule.getMinutes()}`;
				}
			}
		}
		return result;
	}
}
