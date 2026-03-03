import { Routes } from '@angular/router';
import { Login } from './auth/login/login';
import { UsersDashboard } from './users/pages/users-dashboard/users-dashboard';
import { UserForm } from './users/pages/user-form/user-form';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: Login },
  {
    path: 'users',
    canActivate: [authGuard],
    children: [
      { path: '', component: UsersDashboard },
      { path: 'new', component: UserForm },
      { path: 'edit/:id', component: UserForm }
    ]
  },
  { path: '', redirectTo: 'login', pathMatch: 'full' }
];
