import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CalendarComponent } from './calendar.component';
import Day from '../../types/day.type';

describe('CalendarComponent', () => {
  let component: CalendarComponent;
  let fixture: ComponentFixture<CalendarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CalendarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CalendarComponent);
    component = fixture.componentInstance;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should display the days correctly', () => {
    component.days = [
      { day: new Date(2024, 5, 1), slots: [] },
      { day: new Date(2024, 5, 2), slots: [new Date(), new Date()] },
      { day: new Date(2024, 5, 3), slots: [] }
    ];
    component.offset = 1;
    fixture.detectChanges();

    const compiled = fixture.nativeElement;
    const days = compiled.querySelectorAll('#daysgrid > p, #daysgrid > button');

    expect(days.length).toBe(10); // 7 jours + 3 jours dans la matrice des jours
    expect(days[0].textContent).toContain('L');
    expect(days[7].textContent).toContain('1'); // Vérifier le premier jour
    expect(days[8].tagName).toBe('BUTTON'); // Vérifier si le deuxième jour est un bouton
    expect(days[9].textContent).toContain('3'); // Vérifier le troisième jour
  });

  it('should emit onDateSelected and change selectedDay when a day is clicked', () => {
    const day: Day = { day: new Date(2024, 5, 2), slots: [new Date(), new Date()] };
    component.days = [
      { day: new Date(2024, 5, 1), slots: [] },
      day,
      { day: new Date(2024, 5, 3), slots: [] }
    ];
    component.offset = 1;
    fixture.detectChanges();

    spyOn(component.onDateSelected, 'emit');

    const compiled = fixture.nativeElement;
    const secondDayButton = compiled.querySelector('#day2');
    secondDayButton.click();

    fixture.detectChanges();

    expect(component.selectedDay).toBe(day);
    expect(component.onDateSelected.emit).toHaveBeenCalledWith(day);
    expect(secondDayButton.classList).toContain('dayIsSelected');
  });
});
