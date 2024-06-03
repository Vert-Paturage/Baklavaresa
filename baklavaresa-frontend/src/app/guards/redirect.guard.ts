import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { AuthService } from "../services/auth.service";

@Injectable({
	providedIn: 'root'
})
export class RedirectGuard {
	constructor(private auth: AuthService, private router: Router) {}

	canActivate(): boolean {
		if (this.auth.hasValidSecretKey()) {
			this.router.navigate(['/admin']);
			return false;
		}
		return true;
	}
}