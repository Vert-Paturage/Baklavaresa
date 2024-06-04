import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AboutComponent } from './about.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('AboutComponent', () => {
  let component: AboutComponent;
  let fixture: ComponentFixture<AboutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
        imports: [AboutComponent, RouterTestingModule ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AboutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render À propos de nous section', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.bloc:nth-child(1) h2')?.textContent).toContain('À propos de nous');
  });

  it('should render Notre histoire section', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.bloc:nth-child(2) h2')?.textContent).toContain('Notre histoire');
  });

  it('should render Nos horaires section', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.bloc:nth-child(3) h2')?.textContent).toContain('Nos horaires');
  });

  it('should render Où sommes nous ? section', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.bloc:nth-child(4) h2')?.textContent).toContain('Où sommes nous ?');
  });

  it('should render Notre équipe section', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.bloc:nth-child(5) h2')?.textContent).toContain('Notre équipe');
  });

  it('should render Contactez-nous section', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.bloc:nth-child(6) h2')?.textContent).toContain('Contactez-nous');
  });

  it('should have a working reservation button', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    const button = compiled.querySelector('.bloc:nth-child(3) button');
    expect(button).toBeTruthy();
    expect(button?.getAttribute('routerLink')).toBe('/reservation');
  });
});
