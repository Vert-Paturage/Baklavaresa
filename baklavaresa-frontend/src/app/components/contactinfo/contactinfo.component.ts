import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { ApiService } from '../../services/api.service';

import ReservationRequest from "../../types/reservationRequest";

import { getUTCISOString } from "../../utils/dateStringHandler";
import { SnackbarService } from "../../services/snackbar.service";

@Component({
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  selector: "app-contactinfo",
  templateUrl: "./contactinfo.component.html",
  styleUrl: "./contactinfo.component.css",
  providers: [ApiService]
})
export class ContactInfoComponent {
  Reservation: ReservationRequest = {
    firstName: "",
    lastName: "",
    email: "",
    date: "",
    numberOfPeople: 0,
  };

  private selectedDate: Date = new Date();


  constructor(private api: ApiService, private router: Router, private snackBar: SnackbarService) {
    const navigation = this.router.getCurrentNavigation();
    console.log("Navigation: ", navigation);
    if (navigation != null) {
      const nav = navigation.extractedUrl;
      if(nav != null) {
        this.selectedDate = nav.queryParams['state'];
        this.Reservation.numberOfPeople = nav.queryParams['people'];
      }
    }
  }

  submitReservation() {
    if (this.Reservation.numberOfPeople < 0 || this.Reservation.firstName == null || this.Reservation.lastName == null || this.Reservation.email == null || this.Reservation.date == null) {
      this.snackBar.showSnackbar("Veuillez remplir tous les champs", 'error');
    }
    else {
      let date = new Date(this.selectedDate);
      this.Reservation.date = getUTCISOString(date);
      this.Reservation.email = this.Reservation.email.toLowerCase();
        if (this.Reservation != null) {
          this.api.createReservation(this.Reservation).subscribe(
            (response) => {
              this.snackBar.showSnackbar("Réservation ajoutée avec succès", 'success');
              this.Reservation = {
                firstName: "",
                lastName: "",
                email: "",
                date: "",
                numberOfPeople: 0,
              };
            },
            (error) => this.snackBar.showSnackbar(error.error, 'error')
          );
      }
    }
  }
}
