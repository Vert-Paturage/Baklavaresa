import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { ApiService } from '../../services/api.service';

import ReservationRequest from "../../types/reservationRequest";

import { getUTCISOString } from "../../utils/dateStringHandler";

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


  constructor(private api: ApiService, private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    console.log("Navigation: ", navigation);
    if (navigation != null) {
      const nav = navigation.extractedUrl;
      if(nav != null) {
        // this.Reservation.date = nav.queryParams['state'];
        this.selectedDate = nav.queryParams['state'];
        this.Reservation.numberOfPeople = nav.queryParams['people'];
      }
    }
  }

  submitReservation() {
    if (this.Reservation.numberOfPeople > 0 && this.Reservation.firstName != null && this.Reservation.lastName != null && this.Reservation.email) {
	  let date = new Date(this.selectedDate);
      this.Reservation.date = getUTCISOString(date);
      console.log("Reservation: ", this.Reservation);
	  this.Reservation.email = this.Reservation.email.toLowerCase();
      if (this.Reservation != null) {
        const response = this.api.createReservation(this.Reservation).subscribe((response: string) => {
          console.log("Reservation created: ", response);
        })
      }
    }
  }
}
