import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';
import { PlatComponent } from './plat/plat.component';
import { MenuComponent } from './menu/menu.component';
import { CarteComponent } from './carte/carte.component';
import { LoginComponent } from './connexion/login/login.component';
import { HomeComponent } from './connexion/home/home.component';
import { RegisterComponent } from './connexion/register/register.component';
import { ProfileComponent } from './connexion/profile/profile.component';
import { BoardUserComponent } from './connexion/board-user/board-user.component';
import { BoardModeratorComponent } from './connexion/board-moderator/board-moderator.component';
import { BoardAdminComponent } from './connexion/board-admin/board-admin.component';


const routes: Routes = [
  { path: '', redirectTo: '/carte', pathMatch: 'full' },
  { path: 'plat/:id', component: PlatComponent }, 
  { path: 'menu/:id', component: MenuComponent },
  { path: 'carte', component: CarteComponent },
  { path: 'identification', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'user', component: BoardUserComponent },
  { path: 'mod', component: BoardModeratorComponent },
  { path: 'admin', component: BoardAdminComponent },
];


@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
