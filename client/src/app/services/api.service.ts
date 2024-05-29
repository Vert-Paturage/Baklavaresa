import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import Reservation from '../types/reservation.type';
import Calendar from '../types/calendar.type';

@Injectable()
export class ApiService {
	constructor(private http: HttpClient) {}

	getReservations() {
		return this.http.get<string>('/api/Reservation');
	}

	getReservation(id: number) {
		return this.http.get<string>(`/api/Reservation/${id}`);
	}

	createReservation(reservation: Reservation): Observable<string> {
		return this.http.post<string>('/api/Reservation/CreateReservation', reservation);
	}

	deleteReservation(id: number) {
		return this.http.delete<string>(`/api/Reservation/${id}`);
	}

	getTables() {
		return this.http.get<string>('/api/Table');
	}

	getTable(id: number) {
		return this.http.get<string>(`/api/Table/${id}`);
	}

	getCalendar(calendar: Calendar): Map<Date, Date[]> {
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

	getRandomSchedule(day: number): Date {
		const date: Date = new Date(2024,4, day);
		return new Date(date.getFullYear(), date.getMonth(), day, Math.floor(Math.random() * 24), Math.floor(Math.random() * 60));
	}
}