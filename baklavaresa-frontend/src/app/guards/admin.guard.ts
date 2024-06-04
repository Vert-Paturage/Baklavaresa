import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "../services/auth.service";

@Injectable({
	providedIn: 'root'
})
export class AdminGuard {
	constructor(private auth: AuthService, private router: Router) {}

	canActivate(): boolean {
		if (this.auth.hasValidSecretKey()) {
			return true;
		} else {
			this.router.navigate(['/admin/secret']);
			return false;
		}
	}
}