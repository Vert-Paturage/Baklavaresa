import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Reservation from '../Types/reservation.type';
import Horaire from '../Types/horaire.type';
import { Observable } from 'rxjs';

@Injectable()
export class ApiService {
  getDatesDisponibles(): Observable<string[]> {
    throw new Error('Method not implemented.');
  }
	constructor(private http: HttpClient) {}

  reserve(reservation: Reservation): Observable<Horaire> {
    return this.http.post<Horaire>('/api/Reservation', reservation);
  }

	getReservations() {
		return this.http.get<string>('/api/Reservation');
	}

	getReservation(id: number) {
		return this.http.get<string>(`/api/Reservation/${id}`);
	}

	postReservation(reservation: any) {
		return this.http.post<string>('/api/Reservation', reservation);
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
}
