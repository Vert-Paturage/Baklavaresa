import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { AdminComponent } from './admin.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { ApiService } from '../../services/api.service';
import { SnackbarService } from '../../services/snackbar.service';
import { of } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatNativeDateModule } from '@angular/material/core';

describe('AdminComponent', () => {
  let component: AdminComponent;
  let fixture: ComponentFixture<AdminComponent>;
  let apiService: ApiService;
  let snackbarService: SnackbarService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        AdminComponent,
        CommonModule,
        MatFormFieldModule,
        MatInputModule,
        MatDatepickerModule,
        MatNativeDateModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        HttpClientTestingModule,
      ],
      providers: [
        ApiService,
        SnackbarService,
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminComponent);
    component = fixture.componentInstance;
    apiService = TestBed.inject(ApiService);
    snackbarService = TestBed.inject(SnackbarService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should format date correctly', () => {
    const date = new Date('2024-01-01T00:00:00Z');
    expect(component.formatDate(date.toISOString())).toBe('lundi 1 janvier 2024');
  });

  it('should update SelectedDay and retrieve reservations on date change', () => {
    const event = { value: new Date('2024-01-01') };

    spyOn(component, 'retrieveReservationByDate');

    component.onDateChange(event);

    expect(component.SelectedDay).toEqual(new Date('2024-01-01'));

    expect(component.retrieveReservationByDate).toHaveBeenCalledWith(new Date('2024-01-01'));

    expect(component.SelectedDayString).toEqual('lundi 1 janvier 2024'); 
  });

 
  it('should call retrieveReservationByDate on init', fakeAsync(() => {
    spyOn(component, 'retrieveReservationByDate');

    component.ngOnInit();
    tick();

    expect(component.retrieveReservationByDate).toHaveBeenCalled();
  }));
});