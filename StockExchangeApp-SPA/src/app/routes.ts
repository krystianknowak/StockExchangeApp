import { RegisterComponent } from './register/register.component';
import { Routes } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';
import { PanelComponent } from './panel/panel.component';
import { HomeComponent } from './home/home.component';

export const AppRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'register', component: RegisterComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'panel', component: PanelComponent},
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

