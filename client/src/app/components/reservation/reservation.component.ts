import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { Observable, BehaviorSubject } from 'rxjs';

import Reservation from '../../types/reservation.type';

@Component({
  selector: 'app-reservation',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule, RouterOutlet],
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css'], // Correction de styleUrl en styleUrls
  providers: [ApiService]
})
export class ReservationComponent implements OnInit {
  form!: FormGroup;
  numberOfPerson: number = 2;
  availableDate$: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]); // Initialise le tableau des dates disponibles
  availableSchedule$: BehaviorSubject<{ [date: string]: string[] }> = new BehaviorSubject<{ [date: string]: string[] }>({}); // Initialise un objet pour stocker les horaires disponibles pour chaque date

  dateSelected: string | null = null;
  scheduleSelected: string | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private apiService: ApiService
  ) {}

  selectSchedule(date: string, horaire: string) {
    this.dateSelected = date;
    this.scheduleSelected = horaire;
  }

  ngOnInit(): void {
    // Utilisation de BehaviorSubject pour émettre les valeurs initiales
    this.availableDate$.next(['2024-06-07', '2024-05-08', '2024-05-09']);
    this.availableSchedule$.next({
      '2024-06-07': ['10:00', '12:00', '15:00', '16:00', '17:00'],
      '2024-05-08': ['11:00', '13:00', '16:00'],
      '2024-05-09': ['09:00', '14:00', '17:00']
    });

    this.form = this.formBuilder.group({
      prenom: ['', Validators.required],
      nom: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onSubmit() {
    if (this.form.valid) {
      const reservation: Reservation = {
        ID : 0,
        Date : {
          Date: this.dateSelected as string,
          Time: this.scheduleSelected as string
        },
        NumberOfPeople : this.numberOfPerson,
        Tables : [],
        FirstName : this.form.get('prenom')?.value,
        LastName : this.form.get('nom')?.value,
        Email : this.form.get('email')?.value
      };

      console.log(reservation);

      const retour: Observable<string> = this.apiService.createReservation(reservation);

      //get pipe from value
      retour.subscribe(value => console.log(value));

      alert('Votre réservation a été ajoutée avec succès !');

      this.form.reset();
    } else {
      alert('Veuillez remplir correctement tous les champs du formulaire.');
    }
  }
}
