import { Component, OnInit } from '@angular/core';
import {DetalhesRegistroService} from 'src/app/shared/detalhes-registro.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registrar',
  templateUrl: './registrar.component.html',
  styles: []
})
export class RegistrarComponent implements OnInit {

  constructor(private servico:DetalhesRegistroService, private rota: Router) { }

  ngOnInit() {
    console.log(this.servico.formData);
    this.LimparFormulario();
  }

Enviar(form:NgForm)
{
  
  this.Inserir(form);
 
}
  LimparFormulario(form?:NgForm)
  {
    if(form != null)
    form.resetForm();
    this.servico.formData = {
      UsuarioID: 0,
      Nome: '',
      Senha: '',
      ConfirmarSenha: ''
    }
  }

  Inserir(form:NgForm)
  {
   this.servico.postDetalheRegistro().subscribe(
     res => {
       this.LimparFormulario(form);
       this.servico.Listar();
       this.rota.navigateByUrl('/login')
     },
     err => {
       console.log(err);
     }
   )
    
  }

}
