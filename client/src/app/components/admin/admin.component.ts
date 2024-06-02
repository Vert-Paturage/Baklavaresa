import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import Reservation from '../../types/reservation.type';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {provideNativeDateAdapter} from '@angular/material/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { format, set } from 'date-fns';
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
  Reservation: Reservation[] = [];
  Table: Table[] = [];

  constructor(private apiService: ApiService) {
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
    this.SelectedDay = selectedDate;
    this.apiService.getReservationByDate(this.SelectedDay).subscribe(res => {
      this.Reservation = res as Reservation[];
    });
    this.SelectedDayString = this.formatDate(selectedDate);
  }

  deleteReservation(index: number) {
    const confirmDelete = window.confirm("Êtes-vous sûr de vouloir supprimer cette réservation ?");
    if (confirmDelete) {
      console.log(this.Reservation[index].id);
      this.apiService.deleteReservation(this.Reservation[index].id).subscribe(() => {
        this.Reservation.splice(index, 1);
      });
    }
  }

  addTable() {
    this.apiService.createTable(this.TableSeats).subscribe(() => {
      console.log("Table created" + this.TableSeats);
      this.Table.push({id: this.Table[this.Table.length - 1].id+1, capacity: this.TableSeats});
    });
  }

  deleteTable(index: number) {
    const confirmDelete = window.confirm("Êtes-vous sûr de vouloir supprimer cette table ?");
    console.log(this.Table[index].id);
    if (confirmDelete) {
      this.apiService.deleteTable(this.Table[index].id).subscribe(() => {
        this.Table.splice(index, 1);
      });
    }
  }
}