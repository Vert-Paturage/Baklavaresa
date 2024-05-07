import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { ReservationComponent } from './components/reservation/reservation.component';

@Component({
	selector: 'app-root',
	standalone: true,
	imports: [RouterOutlet, FooterComponent, HeaderComponent, ReservationComponent],
	templateUrl: "app.component.html",
	styleUrl: "app.component.css"
})
export class AppComponent {
}
