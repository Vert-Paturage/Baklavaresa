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

	getDays(calendar: Calendar): any[] {
		const today: Date = new Date();

		const offset: number = Math.floor(Math.random() * 7);
		console.log('offset: ' + offset);
		
		const stub = [];
		for (let i = 1; i <= 31; i++) {
			stub.push({
				day: i,
				month: today.getMonth(),
				year: today.getFullYear(),
				index: (i+offset-1) % 7,
				hasRoom: Math.random() > 0.5
			});
		}
		return stub;
	}
}
