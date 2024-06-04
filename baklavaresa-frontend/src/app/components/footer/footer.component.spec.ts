import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FooterComponent } from './footer.component';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';

describe('FooterComponent', () => {
    let component: FooterComponent;
    let fixture: ComponentFixture<FooterComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [FooterComponent, RouterTestingModule]
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(FooterComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should render BaklavaResa title', () => {
        const title = fixture.debugElement.query(By.css('#notices h2')).nativeElement;
        expect(title.textContent).toContain('BaklavaResa');
    });

    it('should render Baklava Industries paragraph', () => {
        const paragraph = fixture.debugElement.query(By.css('#notices p')).nativeElement;
        expect(paragraph.textContent).toContain('Baklava Industries');
    });

    it('should render the admin link', () => {
        const adminLink = fixture.debugElement.query(By.css('a')).nativeElement;
        expect(adminLink.textContent).toContain('Accès administrateur');
        expect(adminLink.getAttribute('routerLink')).toBe('admin/secret');
    });
});
