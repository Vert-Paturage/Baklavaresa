import { Component } from "@angular/core";
import { Router } from "@angular/router";

import { ApiService } from '../../services/api.service';

import Reservation from "../../types/reservation.type";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { first } from "rxjs";

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
		FirstName: "",
		LastName: "",
		Email: "",
		Date: new Date(),
		NumberOfPeople: 0
	};
	  

	constructor(private api: ApiService, private router: Router) {
		const navigation = this.router.getCurrentNavigation();
		if (navigation != null) {
			const state = navigation.extras.state;
			if (state instanceof Date) {
				this.schedule = state;
				console.log("Schedule: ", this.schedule);
			}
		}
	}

	submitReservation() {
		if (this.schedule != null) {
			this.Reservation.Date = this.schedule;
			this.Reservation.NumberOfPeople = 1;
			
			console.log("Reservation: ", this.Reservation);
			if (this.Reservation != null) {
				const response = this.api.createReservation(this.Reservation);
				console.log("Response: ", response);
			}
		}
	}
}