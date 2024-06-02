import { Routes } from '@angular/router';

import { HomeComponent } from './components/home/home.component';
import { AdminComponent } from './components/admin/admin.component';
import { ReservationComponent } from './components/reservation/reservation.component';
import { MenuComponent } from './components/menu/menu.component';

export const routes: Routes = [
	{ path: '', component: HomeComponent },
	{ path: 'admin', component: AdminComponent },
	{ path: 'reservation', component: ReservationComponent },
	{ path: 'menu', component: MenuComponent},
	{ path: '**', redirectTo: '', pathMatch: 'full'}
];
