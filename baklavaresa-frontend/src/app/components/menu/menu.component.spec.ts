import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MenuComponent } from './menu.component';
import { By } from '@angular/platform-browser';

describe('MenuComponent', () => {
    let component: MenuComponent;
    let fixture: ComponentFixture<MenuComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [MenuComponent]
        })
        .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(MenuComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should render menu sections', () => {
        const sections = fixture.debugElement.queryAll(By.css('.menu-section'));
        expect(sections.length).toBe(3);
    });

    it('should render Entrées section', () => {
        const entreesSection = fixture.debugElement.query(By.css('#entrees'));
        expect(entreesSection).toBeTruthy();
        const entreesHeader = entreesSection.query(By.css('h2')).nativeElement;
        expect(entreesHeader.textContent).toContain('Entrées');
    });

    it('should render Plats principaux section', () => {
        const platsSection = fixture.debugElement.query(By.css('#plats-principaux'));
        expect(platsSection).toBeTruthy();
        const platsHeader = platsSection.query(By.css('h2')).nativeElement;
        expect(platsHeader.textContent).toContain('Plats principaux');
    });

    it('should render Desserts section', () => {
        const dessertsSection = fixture.debugElement.query(By.css('#desserts'));
        expect(dessertsSection).toBeTruthy();
        const dessertsHeader = dessertsSection.query(By.css('h2')).nativeElement;
        expect(dessertsHeader.textContent).toContain('Desserts');
    });

    it('should render list items correctly', () => {
        const items = fixture.debugElement.queryAll(By.css('li'));
        expect(items.length).toBe(9);
    });
});
