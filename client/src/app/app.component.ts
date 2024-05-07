import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { ApiService } from './Services/api.service';
import { catchError } from 'rxjs';

@Component({
	selector: 'app-root',
	standalone: true,
	imports: [RouterOutlet],
	templateUrl: "app.component.html",
	styleUrl: "app.component.css",
	providers: [ApiService]
})
export class AppComponent {
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
