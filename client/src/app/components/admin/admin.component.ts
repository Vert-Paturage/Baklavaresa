import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import Reservation from '../../types/reservation.type';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {provideNativeDateAdapter} from '@angular/material/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { format } from 'date-fns';
import { fr } from 'date-fns/locale';

@Component({
  selector: 'admin',
  standalone: true,
  templateUrl: "admin.component.html",
  styleUrl: "admin.component.css",
  providers: [ApiService, DatePipe, provideNativeDateAdapter()],
  imports: [MatFormFieldModule, MatInputModule, MatDatepickerModule, FormsModule, ReactiveFormsModule, CommonModule],
})

export class AdminComponent implements OnInit{
	SelectedDay!: Date;
  SelectedDayString!: string;
  Reservation: Reservation[] = [
    {
      ID: 6,
      FirstName: "John",
      LastName: "Doe",
      Email: "derya@test.fr",
      Date: new Date(),
      NumberOfPeople: 2,
      NumberOfTables: {ID: 1, Capacity: 2}
    }
  ];


    constructor(private apiService: ApiService, private datePipe: DatePipe) {
  	}
  

  formatDate(date: string): string {
    const parsedDate = new Date(date);
    return format(parsedDate, 'EEEE d MMMM yyyy', { locale: fr });
  }

    ngOnInit(): void {
    }

  onDateChange(event: any): void {
    const selectedDate = event.value;
    // this.apiService.createReservation(this.Reservation[0]).subscribe(() => {
    //   console.log("Reservation created");
    // });
    this.SelectedDay = selectedDate;
    this.apiService.getCalendarAdmin(this.SelectedDay).subscribe(map => {
      this.Reservation = map;
    });
    this.SelectedDayString = this.formatDate(selectedDate);
  }

  deleteReservation(index: number) {
    const confirmDelete = window.confirm("Êtes-vous sûr de vouloir supprimer cette réservation ?");
    if (confirmDelete) {
      this.apiService.deleteReservation(this.Reservation[index].ID).subscribe(() => {
        this.Reservation.splice(index, 1);
      });
    }
  }
}