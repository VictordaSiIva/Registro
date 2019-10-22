import { Component, OnInit } from '@angular/core';
import { DetalhesRegistroService } from '../shared/detalhes-registro.service';
import { NgForm } from '@angular/forms';
import {DatePipe} from '@angular/common'
import { stringify } from '@angular/compiler/src/util';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styles: []
})
export class RegistroComponent implements OnInit{
  inicio: any;
  saidaAlmoco: any;
  voltaAlmoco: any;
  saida: any;
  DataInicio: any;
  DataTermino: any;
  UsuarioBuscado: any; 
  usuario: any;
  data:any;
  
  constructor(private servico:DetalhesRegistroService) {  ;
  }

  ngOnInit() {
    
    this.usuario = localStorage.getItem('usuario');
    this.data = localStorage.getItem('data');
    console.log(this.usuario);

  }

  HoraInicio()
  {
    
    this.servico.getNome().subscribe(
      (res:any) => {
        this.servico.postHoraInicio(res).subscribe(
          res => {
          this.inicio = res;
          },
          err => {
            console.log(err);
          }
          );
      },
      err => {
        console.log(err);
      }
    );
  
  }

  SaidaAlmoco()
  {
    this.servico.getNome().subscribe(
      (res:any) => {
        this.servico.postSaidaAlmoco(res).subscribe(
          res => {
            this.saidaAlmoco = res;
            },
          err => {
            console.log(err);
          }
        )
      },
      err => {
        console.log(err);
      }
    )

  }

  VoltaAlmoco()
  {
    this.servico.getNome().subscribe(
      (res:any) => {
        this.servico.postVoltaAlmoco(res).subscribe(
          res => {
            this.voltaAlmoco = res;
            },
          err => {
            console.log(err);
          }
        )
      },
      err => {
        console.log(err);
      }
    )

  }

  HoraSaida()
  {
    this.servico.getNome().subscribe(
      (res:any) => {
        this.servico.postHoraSaida(res).subscribe(
          res => {
            this.saida = res;
            },
          err => {
            console.log(err);
          }
        )
      },
      err => {
        console.log(err);
      }
    )

  }

  Buscar()
  {
    this.servico.Buscar(this.DataInicio, this.DataTermino).subscribe(
      (res:any) => 
      {
        this.UsuarioBuscado = res;
       
       
        console.log(this.UsuarioBuscado);
      }, 
      err => {
        console.log(err);
      }
    )    
  }

}
