import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterOutlet } from '@angular/router';


@Component({
  selector: 'app-reservation',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule, RouterOutlet],
  templateUrl: './reservation.component.html',
  styleUrl: './reservation.component.css'
})
export class ReservationComponent implements OnInit {

  constructor(private formBuilder: FormBuilder) {}
  coordonneesForm!: FormGroup;
  nombrePersonnes: number = 2; 
  datesDisponibles: string[] = []; // Initialise le tableau des dates disponibles
  horairesDisponibles: { [date: string]: string[] } = {}; // Initialise un objet pour stocker les horaires disponibles pour chaque date

  horaireSelected: string | null = null;

  selectHoraire(horaire: string) {
      this.horaireSelected = horaire;
  }

  ngOnInit(): void {
    
    this.datesDisponibles = ['2024-05-07', '2024-05-08', '2024-05-09']; 
    this.horairesDisponibles['2024-05-07'] = ['10:00', '12:00', '15:00', '16:00', '17:00']; 
    this.horairesDisponibles['2024-05-08'] = ['11:00', '13:00', '16:00']; 
    this.horairesDisponibles['2024-05-09'] = ['09:00', '14:00', '17:00'];

    this.coordonneesForm = this.formBuilder.group({
      prenom: ['', Validators.required],
      nom: ['', Validators.required],
      email: ['', Validators.required]
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
