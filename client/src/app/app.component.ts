import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { ApiService } from './Services/api.service';

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

	constructor(private apiService: ApiService) {
		this.apiService.getReservations().subscribe((data) => {
			var p : HTMLParagraphElement = document.getElementById('test') as HTMLParagraphElement;
			p.innerHTML = data.toString();
		});
	}
}
