import { Component } from "@angular/core";
import { Router } from "@angular/router";

import { ApiService } from '../../services/api.service';

import Reservation from "../../types/reservation.type";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import ReservationRequest from "../../types/reservationRequest";

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
      let date = new Date(this.selectedDate); // je deteste le javascript
      this.Reservation.date = date.toISOString();
      console.log("Reservation: ", this.Reservation);
      if (this.Reservation != null) {
        const response = this.api.createReservation(this.Reservation).subscribe((response: string) => {
          console.log("Reservation created: ", response);
        })
      }
    }
  }
}
