import { Injectable } from '@angular/core';
import {Registro} from './RegistroModel';
import{HttpClient} from '@angular/common/http'
import { Horas } from './HorasModel';

@Injectable({
  providedIn: 'root'
})
export class DetalhesRegistroService {

  formData:Registro;
  Horas: Horas; 
  readonly rootURL = 'https://localhost:44320/api';
  lista: Registro[];

  constructor(private http:HttpClient) { }

  postDetalheRegistro()
  {
    return this.http.post(this.rootURL + '/Usuario/', this.formData);
  }

  GetDetalheRegistro()
  {
    return this.http.get(this.rootURL + '/Usuario/' + this.formData.UsuarioID);
  }
  postHoraInicio(usuario: any)
  {
    return this.http.post(this.rootURL + '/Registro/HoraInicio/', usuario);

  }

  postSaidaAlmoco(usuario: any)
  {
    return this.http.post(this.rootURL + '/Registro/SaidaAlmoco/', usuario);
    
  }
  postVoltaAlmoco(usuario: any)
  {
    return this.http.post(this.rootURL + '/Registro/VoltaAlmoco/', usuario);
    
  }
  postHoraSaida(usuario: any)
  {
    return this.http.post(this.rootURL + '/Registro/HoraSaida/', usuario);
    
  }

  getNome()
  {
    return this.http.get(this.rootURL + '/Usuario/Buscar/' + localStorage.getItem('usuario'));
  }

Buscar(dataInicio: any, dataTermino:any)
{
  return this.http.get(this.rootURL + '/Registro/Buscar/' + localStorage.getItem('id') + '/'+ dataInicio + '/' + dataTermino);
}


  Listar()
  {
    this.http.get(this.rootURL + '/Usuario')
    .toPromise()
    .then(res => this.lista = res as Registro[]);
  }

  login(formData)
{
  return this.http.post(this.rootURL+'/Usuario/Login', formData);
}


}
