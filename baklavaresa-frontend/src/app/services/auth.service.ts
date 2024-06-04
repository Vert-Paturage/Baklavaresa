import { Injectable } from "@angular/core";
import { Router } from "@angular/router";

@Injectable({
	providedIn: 'root'
})
export class AuthService {
	private readonly input = '';
	private readonly secretKey = 'lesbaklavascesttropbon';

	constructor(private router: Router) {}
	
	hasValidSecretKey(): boolean {
		return localStorage.getItem(this.input) === this.secretKey;
	}

	setSecretKey(secretKey: string): boolean {
		if (secretKey === this.secretKey) {
			localStorage.setItem(this.input, secretKey);
			return true;
		}
		return false;
	}
	
	clearSecretKey(): void {
		localStorage.removeItem(this.input);
	}
}