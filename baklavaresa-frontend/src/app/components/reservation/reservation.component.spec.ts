import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { By } from '@angular/platform-browser';
import { ReservationComponent } from './reservation.component';
import { CalendarComponent } from '../calendar/calendar.component';
import { ScheduleSelectorComponent } from '../scheduleselector/scheduleselector.component';
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule
import { ApiService } from '../../services/api.service';
import { of } from 'rxjs';
import { Router } from '@angular/router';
import { Component } from '@angular/core';

describe('ReservationComponent', () => {
  let component: ReservationComponent;
  let fixture: ComponentFixture<ReservationComponent>;
  let apiService: jasmine.SpyObj<ApiService>;
  let router: Router;

  beforeEach(async () => {
    const apiServiceSpy = jasmine.createSpyObj('ApiService', ['getCalendar']);

    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientModule, // Add HttpClientModule
        ReservationComponent,
        CalendarComponent,
        ScheduleSelectorComponent
      ],
      providers: [{ provide: ApiService, useValue: apiServiceSpy }]
    }).compileComponents();

    apiService = TestBed.inject(ApiService) as jasmine.SpyObj<ApiService>;
    router = TestBed.inject(Router);
    fixture = TestBed.createComponent(ReservationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should update SelectedDay on date selection', () => {
    const day = { day: new Date(), slots: [] };
    component.onDateSelected(day);
    expect(component.SelectedDay).toBe(day);
  });

  it('should update SelectedSchedule on schedule selection', () => {
    const schedule = new Date();
    component.onScheduleSelected(schedule);
    expect(component.SelectedSchedule).toBe(schedule);
  });

  });

    @Component({
      template: ''
    })
    class DummyComponent {}

