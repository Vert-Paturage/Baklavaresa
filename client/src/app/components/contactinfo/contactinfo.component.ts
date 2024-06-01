import { Component } from "@angular/core";
import { Router } from "@angular/router";

import { ApiService } from "../../services/api.service";

import Reservation from "../../types/reservation.type";

@Component({
	standalone: true,
	selector: "app-contactinfo",
	templateUrl: "./contactinfo.component.html",
	styleUrl: "./contactinfo.component.css"
})
export class ContactInfoComponent {
	Reservation: Reservation | null = null;
	schedule: Date | null = null;

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
		if(this.Reservation != null) {
			const response = this.api.createReservation(this.Reservation);
			console.log("Response: ", response);
		}
	}
}