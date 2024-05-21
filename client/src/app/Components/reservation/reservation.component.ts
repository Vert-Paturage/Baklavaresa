import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { ApiService } from '../../Services/api.service';
import { Observable, BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-reservation',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule, RouterOutlet],
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css'], // Correction de styleUrl en styleUrls
  providers: [ApiService]
})
export class ReservationComponent implements OnInit {
  coordonneesForm!: FormGroup;
  nombrePersonnes: number = 2;
  datesDisponibles$: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]); // Initialise le tableau des dates disponibles
  horairesDisponibles$: BehaviorSubject<{ [date: string]: string[] }> = new BehaviorSubject<{ [date: string]: string[] }>({}); // Initialise un objet pour stocker les horaires disponibles pour chaque date

  horaireSelected: string | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private apiService: ApiService
  ) {}

  selectHoraire(horaire: string) {
    this.horaireSelected = horaire;
  }

  ngOnInit(): void {
    // Utilisation de BehaviorSubject pour émettre les valeurs initiales
    this.datesDisponibles$.next(['2024-06-07', '2024-05-08', '2024-05-09']);
    this.horairesDisponibles$.next({
      '2024-06-07': ['10:00', '12:00', '15:00', '16:00', '17:00'],
      '2024-05-08': ['11:00', '13:00', '16:00'],
      '2024-05-09': ['09:00', '14:00', '17:00']
    });

    this.coordonneesForm = this.formBuilder.group({
      prenom: ['', Validators.required],
      nom: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onSubmit() {
    if (this.coordonneesForm.valid) {
      alert('Votre réservation a été ajoutée avec succès !');
      this.coordonneesForm.reset();
    } else {
      alert('Veuillez remplir correctement tous les champs du formulaire.');
    }
  }
}