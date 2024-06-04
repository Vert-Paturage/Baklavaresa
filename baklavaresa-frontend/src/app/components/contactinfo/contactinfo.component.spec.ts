import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactInfoComponent } from './contactinfo.component';
import { By } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ApiService } from '../../services/api.service';
import { of, throwError } from 'rxjs';

describe('ContactInfoComponent', () => {
  let component: ContactInfoComponent;
  let fixture: ComponentFixture<ContactInfoComponent>;

  beforeEach(async () => {
    const apiServiceSpy = jasmine.createSpyObj('ApiService', ['getCalendar']);
    await TestBed.configureTestingModule({
      imports: [ContactInfoComponent,
                HttpClientModule
      ],
      providers: [{ provide: ApiService, useValue: apiServiceSpy }]
    }).compileComponents();
    
    fixture = TestBed.createComponent(ContactInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should not submit the form if any required field is empty', () => {
    // Fill out all fields except one
    component.Reservation.firstName = 'John';
    component.Reservation.lastName = 'Doe';
    fixture.detectChanges();

    const submitButton = fixture.debugElement.query(By.css('button[type="submit"]')).nativeElement;
    submitButton.click();
    fixture.detectChanges();

    expect((component.Reservation as any).valid).toBeFalsy();
  });  

});