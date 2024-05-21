import { Routes } from '@angular/router';

import { HomeComponent } from './Components/Home/home.component';
import { AdminComponent } from './Components/Admin/admin.component';
import { ReservationComponent } from './Components/reservation/reservation.component';

export const routes: Routes = [
	{ path: '', component: HomeComponent },
	{ path: 'admin', component: AdminComponent },
	{ path: 'reservation', component: ReservationComponent },

];
