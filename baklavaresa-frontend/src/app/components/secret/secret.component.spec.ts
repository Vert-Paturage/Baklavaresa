import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SecretComponent } from './secret.component';

describe('SecretComponent', () => {
    let component: SecretComponent;
    let fixture: ComponentFixture<SecretComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [SecretComponent]
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(SecretComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should render the secret message', () => {
        const message = fixture.nativeElement.querySelector('div');
        expect(message.textContent).toContain('Entrez le mot de passe :');
    });

    it('should render the password input', () => {
        const input = fixture.nativeElement.querySelector('input');
        expect(input).toBeTruthy();
    });

    it('should render the submit button', () => {
        const button = fixture.nativeElement.querySelector('button');
        expect(button.textContent).toContain('Valider');
    });

    it('should render the error message', () => {
        component.error = 'Mot de passe incorrect';
        fixture.detectChanges();
        const errorMessage = fixture.nativeElement.querySelector('div');
        expect(errorMessage.textContent).toContain('Mot de passe incorrect');
    });
});
