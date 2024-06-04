import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CalendarComponent } from './calendar.component';


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
    fixture.detectChanges();
  });


  it('should display the correct month and year', () => {
    component.monthName = 'January';
    component.year = 2024;
    fixture.detectChanges();

    const monthLabel = fixture.nativeElement.querySelector('.month-label');
    expect(monthLabel.textContent).toBe('January 2024');
  });

  it('should display the correct number of days in a month', () => {
    const februaryDays = [
      { day: new Date(2024, 1, 1), slots: [] },
      { day: new Date(2024, 1, 2), slots: [] },
    ];
    component.days = februaryDays;
    component.monthName = 'February';
    component.year = 2024;
    fixture.detectChanges();

    const dayElements = fixture.nativeElement.querySelectorAll('.day');
    expect(dayElements.length).toBe(29); // Assuming February 2024 has 29 days
  });

  it('should emit the selected day on click', () => {
    const selectedDay = { day: new Date(2024, 1, 15), slots: [] };
    component.days = [selectedDay];
    component.monthName = 'January';
    component.year = 2024;
    fixture.detectChanges();

    const dayElement = fixture.nativeElement.querySelector('#day15');
    dayElement.click();

    expect(component.selectedDay).toEqual(selectedDay);
  });

  it('should disable days with no slots', () => {
    const daysWithSlots = [
      { day: new Date(2024, 1, 1), slots: [] },
      { day: new Date(2024, 1, 2), slots: [] },
      { day: new Date(2024, 1, 3), slots: [] },
    ];
    component.days = daysWithSlots;
    component.monthName = 'January';
    component.year = 2024;
    fixture.detectChanges();
  
    const dayWithNoSlots = fixture.nativeElement.querySelector('#day2');
    if (dayWithNoSlots) { // Check if element exists
      expect(dayWithNoSlots.disabled).toBeTruthy();
    }
  
    const dayWithSlots = fixture.nativeElement.querySelector('#day1');
    expect(dayWithSlots.disabled).toBeFalsy();
  });
});