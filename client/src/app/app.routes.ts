import { Routes } from '@angular/router';

// Import the Home and Admin components
import { HomeComponent } from './Components/Home/home.component';
import { AdminComponent } from './Components/Admin/admin.component';

export const routes: Routes = [
	{ path: '', component: HomeComponent },
	{ path: 'admin', component: AdminComponent },
	{ path: '**', redirectTo: '' }
];
