import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FooterComponent } from './Components/footer/footer.component';
import { HeaderComponent } from './Components/header/header.component';
import { ReservationComponent } from './Components/reservation/reservation.component';

@Component({
	selector: 'app-root',
	standalone: true,
	imports: [RouterOutlet, FooterComponent, HeaderComponent, ReservationComponent],
	templateUrl: "app.component.html",
	styleUrl: "app.component.css"
})
export class AppComponent {
}
