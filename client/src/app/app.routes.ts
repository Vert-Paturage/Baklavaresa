import { Routes } from '@angular/router';

// Import the Home and Admin components
import { HomeComponent } from './Components/home/home.component';
import { AdminComponent } from './Components/admin/admin.component';

export const routes: Routes = [
	{ path: '', component: HomeComponent },
	{ path: 'admin', component: AdminComponent },
	{ path: '**', redirectTo: '' }
];
