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
import Table from '../../types/table.type';

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
  TableNumber!: number;
  TableSeats!: number;
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

  Table: Table[] = [
    {
      ID: 1,
      Capacity: 2
    },
    {
      ID: 2,
      Capacity: 4
    },
    {
      ID: 3,
      Capacity: 6
    }
  ];


  constructor(private apiService: ApiService, private datePipe: DatePipe) {
  }
  
  ngOnInit(): void {
    this.apiService.getAllTables().subscribe(table => {
      this.Table = table;
    });
  }

  formatDate(date: string): string {
    const parsedDate = new Date(date);
    return format(parsedDate, 'EEEE d MMMM yyyy', { locale: fr });
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

  addTable() {
    this.apiService.addTable(this.TableSeats).subscribe(() => {
      console.log("Table created");
    });
  }

  deleteTable(index: number) {
    const confirmDelete = window.confirm("Êtes-vous sûr de vouloir supprimer cette table ?");
    if (confirmDelete) {
      this.Table.splice(index, 1);
    }
  }
}