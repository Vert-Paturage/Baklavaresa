import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ApiService {
	constructor(private http: HttpClient) {}

	getReservations() {
		return this.http.get<string>('http://localhost:5017/Reservation');
	}

	getReservation(id: number) {
		return this.http.get<string>(`http://localhost:5017/Reservation/${id}`);
	}

	postReservation(reservation: any) {
		return this.http.post<string>('http://localhost:5017/Reservation', reservation);
	}

	deleteReservation(id: number) {
		return this.http.delete<string>(`http://localhost:5017/Reservation/${id}`);
	}

	getTables() {
		return this.http.get<string>('http://localhost:5017/Table');
	}

	getTable(id: number) {
		return this.http.get<string>(`http://localhost:5017/Table/${id}`);
	}
}
