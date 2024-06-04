import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';

describe('AuthService', () => {
    let service: AuthService;
    let routerSpy: jasmine.SpyObj<Router>;

    beforeEach(() => {
        const spy = jasmine.createSpyObj('Router', ['navigate']);

        TestBed.configureTestingModule({
            providers: [
                AuthService,
                { provide: Router, useValue: spy }
            ]
        });

        service = TestBed.inject(AuthService);
        routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
    });

    afterEach(() => {
        localStorage.clear();
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should return true if the correct secret key is set', () => {
        localStorage.setItem('', 'lesbaklavascesttropbon');
        expect(service.hasValidSecretKey()).toBeTrue();
    });

    it('should return false if the secret key is incorrect', () => {
        localStorage.setItem('', 'wrongsecret');
        expect(service.hasValidSecretKey()).toBeFalse();
    });

    it('should set the secret key and return true if the key is correct', () => {
        const result = service.setSecretKey('lesbaklavascesttropbon');
        expect(result).toBeTrue();
        expect(localStorage.getItem('')).toBe('lesbaklavascesttropbon');
    });

    it('should not set the secret key and return false if the key is incorrect', () => {
        const result = service.setSecretKey('wrongsecret');
        expect(result).toBeFalse();
        expect(localStorage.getItem('')).toBeNull();
    });

    it('should clear the secret key', () => {
        localStorage.setItem('', 'lesbaklavascesttropbon');
        service.clearSecretKey();
        expect(localStorage.getItem('')).toBeNull();
    });
});
