import { Injectable, input } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map } from 'rxjs';

import Reservation from '../types/reservation.type';
import Calendar from '../types/calendar.type';
import Day from '../types/day.type';
import ReservationRequest from '../types/reservationRequest';
import Table from '../types/table.type';

@Injectable()
export class ApiService {

	constructor(private http: HttpClient) {}

	createReservation(reservation: ReservationRequest): Observable<string> {
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

	getReservationByDate(date: Date): Observable<Reservation[]> {
    
		const nextDayUTCDate = new Date(date);
		nextDayUTCDate.setUTCDate(nextDayUTCDate.getUTCDate() + 1);
    	const utcDateString = nextDayUTCDate.toISOString().split('T')[0];

		console.log(utcDateString);
		return this.http.get<Reservation[]>('/api/Reservation/GetAllReservations', {params: {input: utcDateString }})
		.pipe(
			map((reservations: Reservation[]) => {
				return reservations.map(reservation => {
					return {
						id: reservation.id,
						firstName: reservation.firstName,
						lastName: reservation.lastName,
						email: reservation.email,
						date: reservation.date,
						numberOfPeople: reservation.numberOfPeople,
						table: reservation.table
					};
				});
			})
		);
	}

	deleteReservation(id: number): Observable<any> {
		return this.http.delete<string>('/api/Reservation/' + id)
	}


	deleteTable(id: number): Observable<any> {
		return this.http.delete<string>('/api/Table/' + id)
	}

	createTable(tableCapacity: number): Observable<string> {
		const body = { capacity: tableCapacity };
		return this.http.post<string>('/api/Table/Create', body);
	}

	getAllTables(): Observable<Table[]> {
		return this.http.get<Table[]>('/api/Table/GetAll');
	}
}
