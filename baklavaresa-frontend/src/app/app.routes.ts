import { Routes } from '@angular/router';

import { HomeComponent } from './components/home/home.component';
import { AdminComponent } from './components/admin/admin.component';
import { ReservationComponent } from './components/reservation/reservation.component';
import { ContactInfoComponent } from './components/contactinfo/contactinfo.component';
import { MenuComponent } from './components/menu/menu.component';
import { SecretComponent } from './components/secret/secret.component';

import { AdminGuard } from './guards/admin.guard';
import { RedirectGuard } from './guards/redirect.guard';

export const routes: Routes = [
	{ path: '', component: HomeComponent },
	{ path: 'admin', component: AdminComponent, canActivate: [AdminGuard]},
	{ path: 'admin/secret', component: SecretComponent, canActivate: [RedirectGuard]},
	{ path: 'reservation', component: ReservationComponent },
	{ path: 'reservation/validate', component: ContactInfoComponent},
	{ path: 'menu', component: MenuComponent},
	{ path: '**', redirectTo: '', pathMatch: 'full'}
];
