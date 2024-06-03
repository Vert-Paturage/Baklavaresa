import { Component } from "@angular/core";
import { AuthService } from "../../services/auth.service";
import { Router } from "@angular/router";
import { FormsModule } from "@angular/forms";

@Component({
	selector: 'app-secret',
	standalone: true,
	templateUrl: './secret.component.html',
	styleUrl: './secret.component.css',
	imports: [FormsModule]
})
export class SecretComponent {
	secretKeyInput: string = '';
	error: string = '';

	constructor(private auth: AuthService, private router: Router) {}

	submitSecretKey(): void {
		if(this.auth.setSecretKey(this.secretKeyInput)) {
			this.router.navigate(['/admin']);
		}
		else {
			this.error = 'Mot de passe invalide';
		}
	}
}