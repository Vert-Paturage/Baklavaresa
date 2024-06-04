import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HomeComponent } from './home.component';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';

describe('HomeComponent', () => {
    let component: HomeComponent;
    let fixture: ComponentFixture<HomeComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [HomeComponent, RouterTestingModule]
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(HomeComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should display the title "Baklava"', () => {
        const titleElement = fixture.debugElement.query(By.css('#bigtitle')).nativeElement;
        expect(titleElement.textContent).toBe('Baklava');
    });

    it('should display the lure text', () => {
        const lureTextElement = fixture.debugElement.query(By.css('#luretext')).nativeElement;
        expect(lureTextElement.textContent).toContain('De bons Baklava pour votre estomac');
    });

    it('should have a button with text "Réservation"', () => {
        const buttonElement = fixture.debugElement.query(By.css('#toreservationbutton')).nativeElement;
        expect(buttonElement.textContent).toBe('Réservation');
    });

    it('should have a button that navigates to "reservation"', () => {
        const buttonElement = fixture.debugElement.query(By.css('#toreservationbutton')).nativeElement;
        expect(buttonElement.getAttribute('ng-reflect-router-link')).toBe('reservation');
    });
});
