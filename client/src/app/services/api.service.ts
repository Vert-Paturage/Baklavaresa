import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Reservation from '../types/reservation.type';
import { Observable } from 'rxjs';

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
}
