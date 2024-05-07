import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { ApiService } from '../../Services/api.service';

@Component({
	selector: 'app-home',
	standalone: true,
	imports: [RouterOutlet],
	templateUrl: "home.component.html",
	styleUrl: "home.component.css",
	providers: [ApiService]
})
export class HomeComponent {
	title = 'baklava';
	reservations!: string;

	constructor(private apiService: ApiService) {
		this.apiService.getReservations().subscribe(
		  (data) => {
			//Mettre à jour le contenu de l'élément HTML avec l'identifiant "test"
			var p : HTMLParagraphElement = document.getElementById('test') as HTMLParagraphElement;
			p.innerHTML = data.toString();
			console.log(data);
		  },
		  (error) => {
			console.error('Erreur lors de la récupération des réservations : ', error);
			// Gérer l'erreur ici
		  }
		);
	  }
}
