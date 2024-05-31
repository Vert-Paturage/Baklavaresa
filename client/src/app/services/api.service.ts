import { Injectable, input } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map } from 'rxjs';

import Reservation from '../types/reservation.type';
import Calendar from '../types/calendar.type';
import Day from '../types/day.type';
import Table from '../types/table.type';

@Injectable()
export class ApiService {

	constructor(private http: HttpClient) {}

	createReservation(reservation: Reservation): Observable<string> {
		return this.http.post<string>('/api/Reservation/Create', reservation);
	}

	getCalendar(calendar: Calendar): Observable<Day[]> {
		return this.http.post<Day[]>('/api/Reservation/GetAvailableSlots', {NumberOfPeople: calendar.PeopleNumber, Date: calendar.Date})
		.pipe(
			map((days: Day[]) => {
				return days.map(day => {
					return {
						day: new Date(day.day),
						slots: day.slots.map(slot => new Date(slot))
					};
				});
			})
		);
	}

	getCalendarAdmin(date: Date): Observable<Reservation[]> {
		return this.http.get<Reservation[]>('/api/Reservation/GetAllReservations', {params: {input: date.toISOString()}});
	}

	getAllTables(): Observable<Table[]> {
		return this.http.get<Table[]>('/api/Table/GetAll');
	}

	addTable(tableCapacity: number): Observable<string> {
		return this.http.post<string>('/api/Table/Create', tableCapacity);
	}

	getCalendarStub(calendar: Calendar): Map<Date, Date[]> {
		// 1st of may 2024
		const date: Date = new Date(2024, 4, 1);

		const offset: number = Math.floor(Math.random() * 7);
		
		const stub: Map<Date, Date[]> = new Map();
		for (let i = 1; i <= 31; i++) {
			const toSet: Date = new Date(date.getFullYear(), date.getMonth(), i);
			stub.set(toSet, []);
			for (let j = 0; j < Math.floor(Math.random() * 5); j++) {
				stub.get(toSet)!.push(this.getRandomSchedule(i));
			}
		}
		return stub;
	}

	deleteReservation(id: number): Observable<string> {
		return this.http.post<string>('/api/Reservation/Delete', {ID: id});
	}

	private getRandomSchedule(day: number): Date {
		const date: Date = new Date();
		return new Date(date.getFullYear(), date.getMonth(), day, Math.floor(Math.random() * 24), Math.floor(Math.random() * 60));
	}
}