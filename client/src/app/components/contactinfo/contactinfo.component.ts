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
		firstName: "",
		lastName: "",
		email: "",
		date: new Date(),
		numberOfPeople: 0,
		id: 0,
		table: 0
	};
	  

	constructor(private api: ApiService, private router: Router) {
		const navigation = this.router.getCurrentNavigation();
		console.log("Navigation: ", navigation);
		if (navigation != null) {
			const nav = navigation.extractedUrl;
			if(nav != null) {
				this.Reservation.date = nav.queryParams['state'];
				this.Reservation.numberOfPeople = nav.queryParams['people'];
			}
		}
	}

	submitReservation() {
		if (this.Reservation.date != null && this.Reservation.numberOfPeople > 0 && this.Reservation.firstName != null && this.Reservation.lastName != null && this.Reservation.email) {
			console.log("Reservation: ", this.Reservation);
			if (this.Reservation != null) {
				const response = this.api.createReservation(this.Reservation);
				console.log("Response: ", response);
			}
		}
	}
}