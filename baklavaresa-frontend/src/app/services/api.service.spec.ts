import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ApiService } from './api.service';
import Table from '../types/table.type';
import ReservationRequest from '../types/reservationRequest';
import Calendar from '../types/calendar.type';
import { Day } from 'date-fns';

describe('ApiService', () => {
  let service: ApiService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ApiService]
    });
    service = TestBed.inject(ApiService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); 
  });

  it('should delete a reservation by id', () => {
    const id = 1;

    service.deleteReservation(id).subscribe();

    const req = httpMock.expectOne('/api/Reservation/' + id);
    expect(req.request.method).toBe('DELETE');

    req.flush({});

    httpMock.verify();
  });

  it('should delete a table by id', () => {
    const id = 1;

    service.deleteTable(id).subscribe();

    const req = httpMock.expectOne('/api/Table/' + id);
    expect(req.request.method).toBe('DELETE');

    req.flush({});

    httpMock.verify();
  });


  it('should create a table with given capacity', () => {
    const tableCapacity = 4;
    const expectedResponse = 'Success';

    service.createTable(tableCapacity).subscribe(response => {
      expect(response).toEqual(expectedResponse); 
    });

    const req = httpMock.expectOne('/api/Table/Create');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ capacity: tableCapacity });

    req.flush(expectedResponse);

    httpMock.verify();
  });

  it('should get all tables', () => {
    const expectedTables: Table[] = [
      { id: 1, capacity: 4 },
      { id: 2, capacity: 6 },
    ];

    service.getAllTables().subscribe(tables => {
      expect(tables).toEqual(expectedTables); 
    });

    const req = httpMock.expectOne('/api/Table/GetAll');
    expect(req.request.method).toBe('GET');

    req.flush(expectedTables);

    httpMock.verify();
  });

  it('should create a reservation', () => {
    const reservationRequest: ReservationRequest = {
        firstName: 'John',
        lastName: 'Doe',
        email: 'johndoe@example.com',
        date: '2024-01-01 12:00:00',
        numberOfPeople: 4
    };

    const expectedResponse = 'Success'; 

    service.createReservation(reservationRequest).subscribe(response => {
        expect(response).toEqual(expectedResponse); 
    });

    const req = httpMock.expectOne('/api/Reservation/Create');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(reservationRequest);

    req.flush(expectedResponse);

    httpMock.verify();
  });
});
