import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { By } from '@angular/platform-browser';
import { HeaderComponent } from './header.component';
import { Router } from '@angular/router';
import { SpyLocation } from '@angular/common/testing';
import { Component } from '@angular/core';
import { Location } from '@angular/common';

describe('HeaderComponent', () => {
  let component: HeaderComponent;
  let fixture: ComponentFixture<HeaderComponent>;
  let router: Router;
  let location: SpyLocation;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule.withRoutes([
          { path: 'reservation', component: DummyComponent },
          { path: 'menu', component: DummyComponent },
          { path: 'about', component: DummyComponent }
        ]),
        HeaderComponent // Importer le composant autonome ici
      ],
      providers: [{ provide: Location, useClass: SpyLocation }]
    }).compileComponents();

    router = TestBed.inject(Router);
    location = TestBed.inject(Location) as SpyLocation;
    fixture = TestBed.createComponent(HeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display the title', () => {
    const titleElement = fixture.debugElement.query(By.css('#title h1')).nativeElement;
    expect(titleElement.textContent).toContain('Baklava');
  });

  it('should display the logo with correct src', () => {
    const logoElement = fixture.debugElement.query(By.css('#logo')).nativeElement;
    expect(logoElement.src).toContain('assets/baklavalogo.png');
  });

  it('should display the logo with correct alt attribute', () => {
    const logoElement = fixture.debugElement.query(By.css('#logo')).nativeElement;
    expect(logoElement.alt).toBe('Logo');
  });

  it('should have correct navigation links', () => {
    const links = fixture.debugElement.queryAll(By.css('#links a'));
    expect(links.length).toBe(3);

    expect(links[0].nativeElement.getAttribute('routerLink')).toBe('reservation');
    expect(links[0].nativeElement.textContent).toContain('Réservation');

    expect(links[1].nativeElement.getAttribute('routerLink')).toBe('menu');
    expect(links[1].nativeElement.textContent).toContain('Menu');

    expect(links[2].nativeElement.getAttribute('routerLink')).toBe('about');
    expect(links[2].nativeElement.textContent).toContain('Restaurant');
  });

  it('should apply active-link class to active link', async () => {
    const links = fixture.debugElement.queryAll(By.css('#links a'));

    await router.navigate(['/reservation']);
    fixture.detectChanges();
    let activeLinkElement = fixture.debugElement.query(By.css('a.active-link'));
    expect(activeLinkElement.nativeElement.textContent).toContain('Réservation');

    await router.navigate(['/menu']);
    fixture.detectChanges();
    activeLinkElement = fixture.debugElement.query(By.css('a.active-link'));
    expect(activeLinkElement.nativeElement.textContent).toContain('Menu');

    await router.navigate(['/about']);
    fixture.detectChanges();
    activeLinkElement = fixture.debugElement.query(By.css('a.active-link'));
    expect(activeLinkElement.nativeElement.textContent).toContain('Restaurant');
  });

  it('should navigate to the correct routes on link click', async () => {
    const links = fixture.debugElement.queryAll(By.css('#links a'));

    links[0].nativeElement.click();
    await fixture.whenStable();
    expect(location.path()).toBe('/reservation');

    links[1].nativeElement.click();
    await fixture.whenStable();
    expect(location.path()).toBe('/menu');

    links[2].nativeElement.click();
    await fixture.whenStable();
    expect(location.path()).toBe('/about');
  });
});

// Composant factice pour les routes de test
@Component({
  template: ''
})
class DummyComponent {}
