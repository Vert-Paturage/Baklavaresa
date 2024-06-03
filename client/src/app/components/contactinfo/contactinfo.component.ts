import { Component } from "@angular/core";
import { Router } from "@angular/router";

import { ApiService } from '../../services/api.service';

import Reservation from "../../types/reservation.type";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

@Component({
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  selector: "app-contactinfo",
  templateUrl: "./contactinfo.component.html",
  styleUrl: "./contactinfo.component.css",
  providers: [ApiService]
})
export class ContactInfoComponent {
  schedule: Date | null = null;
  Reservation: Reservation = {
    firstName: "",
    lastName: "",
    email: "",
    // date: new Date(),
    date: "",
    numberOfPeople: 0,
  };

  selectedDate: Date = new Date();


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
    if (this.Reservation.date != null && this.Reservation.numberOfPeople > 0 && this.Reservation.firstName != null && this.Reservation.lastName != null && this.Reservation.email) {
      // this.Reservation.date = new Date("2024-06-03T11:12:35.927Z");
      let connard = new Date(this.selectedDate);
      this.Reservation.date = connard.toLocaleDateString();
      if (this.Reservation != null) {
        console.log("Reservation: ", this.Reservation);
        const response = this.api.createReservation(this.Reservation).subscribe( res => {
          console.log("Response");
          console.log(res);
        });
      }
    }
  }
}
