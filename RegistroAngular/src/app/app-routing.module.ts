import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import{RegistrarComponent} from './registrar/registrar.component';
import{LoginComponent} from './login/login.component';
import { RegistroComponent } from './registro/registro.component';


const routes: Routes = [
{path:'', redirectTo:'/registrar', pathMatch:'full'},
{path:'registrar', component:RegistrarComponent},
{path:'login',  component:LoginComponent},
{path:'registro', component:RegistroComponent}


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
