import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ApiService {
	constructor(private http: HttpClient) {}

	getReservations() {
		return this.http.get('http://localhost:5017/Reservation');
	}
}
