import { Component } from "@angular/core";
import { RouterLink	} from "@angular/router";

@Component({
	selector: 'app-about',
	standalone: true,
	templateUrl: './about.component.html',
	styleUrl: './about.component.css',
	imports: [RouterLink]
})
export class AboutComponent {
}