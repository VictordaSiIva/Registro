import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Registro } from '../shared/RegistroModel';
import { DetalhesRegistroService } from '../shared/detalhes-registro.service';
import { Router } from '@angular/router';
import { RegistroComponent } from '../registro/registro.component';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {
  formData:Registro;
  usuario: any;
  constructor(private servico:DetalhesRegistroService, private rota: Router) { }

  ngOnInit() {
    
  }

  Enviar(form:NgForm)
  {

    this.servico.login(form.value).subscribe(
      (res:any)=>{
        localStorage.setItem('token', res.token);
        localStorage.setItem('usuario', res.usuario);
        localStorage.setItem('id', res.id);
        localStorage.setItem('data', res.data);
        
       this.usuario = res;
       console.log(this.usuario);
        this.rota.navigateByUrl('/registro');
     
      },
      err =>{
        if(err.status == 400)
        console.log(err);
      }
    )

  }

}
