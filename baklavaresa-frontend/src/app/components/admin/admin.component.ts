import { Component, OnInit } from '@angular/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { provideNativeDateAdapter } from '@angular/material/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { format } from 'date-fns';
import { fr } from 'date-fns/locale';

import { SnackbarService } from '../../services/snackbar.service';
import { ApiService } from '../../services/api.service';

import Reservation from '../../types/reservation.type';
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
	SelectedDay: Date = new Date();
  SelectedDayString: string = "jour : " + this.formatDate(new Date().toISOString());
  TableNumber!: number;
  TableSeats!: number;
  Reservation: Reservation[] = [];
  Table: Table[] = [];

  constructor(private apiService: ApiService, private snackBar: SnackbarService) {
  }
  
  ngOnInit(): void {
    this.refreshData();
  }

  refreshData() {
    this.apiService.getAllTables().subscribe(table => {
      this.Table = table;
    });
    this.retrieveReservationByDate(this.SelectedDay);
  }


  formatDate(date: string): string {
    const parsedDate = new Date(date);
    return format(parsedDate, 'EEEE d MMMM yyyy', { locale: fr });
  }

  onDateChange(event: any): void {
    const selectedDate = event.value;
    this.SelectedDay = selectedDate;
    this.retrieveReservationByDate(this.SelectedDay);
    this.SelectedDayString = this.formatDate(selectedDate);
  }

  retrieveReservationByDate(date: Date) {
    this.apiService.getReservationByDate(date).subscribe(res => {
      this.Reservation = res as Reservation[];
    });
    
  }

  deleteReservation(index: number) {
    const confirmDelete = window.confirm("Êtes-vous sûr de vouloir supprimer cette réservation ?");
    if (confirmDelete) {
      console.log(this.Reservation[index].id);
      this.apiService.deleteReservation(this.Reservation[index].id).subscribe(
        (response) => {
          this.snackBar.showSnackbar("Réservation supprimée", 'success');
          this.refreshData();
        },
        (error) => this.snackBar.showSnackbar(error.error, 'error')
      );
    }
  }

  addTable() {
    if(this.TableSeats == null || this.TableSeats == undefined) {
      this.snackBar.showSnackbar("La capacité de la table ne peut pas être nulle", 'error');
      return;
    }
    if(this.TableSeats <= 0) {
      this.snackBar.showSnackbar("La capacité de la table doit être supérieure à 0", 'error');
      return;
    }
    this.apiService.createTable(this.TableSeats).subscribe(
      (response) => {
        console.log("Table created" + this.TableSeats);
        this.snackBar.showSnackbar("Table ajoutée", 'success');
        this.TableSeats = 0;
        this.refreshData();
      },
      (error) => this.snackBar.showSnackbar(error.error, 'error')
    );
  }

  deleteTable(index: number) {
    const confirmDelete = window.confirm("Êtes-vous sûr de vouloir supprimer cette table ?");
    console.log(this.Table[index].id);
    if (confirmDelete) {
      this.apiService.deleteTable(this.Table[index].id).subscribe(
        (response) => {
          this.snackBar.showSnackbar("Table supprimée", 'success');
          this.refreshData();
        },
        (error) => this.snackBar.showSnackbar(error.error, 'error')
      );
    }
  }
}