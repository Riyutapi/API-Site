import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Response } from '../Models/Response';
import { Departamento } from '../Models/Departamentos';
import { Funcionario } from '../Models/Funcionarios';
import { HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})

export class CrudService {

  constructor(private http: HttpClient) { }

  // URL da API para operações relacionadas a Departamentos
  private apiUrlD = `${environment.ApiUrl}/Departamento`;

  // Método para obter lista de todos os Departamentos
  GetDepartamentos(): Observable<Response<Departamento[]>> {
    return this.http.get<Response<Departamento[]>>(this.apiUrlD);
  }

  // Método para obter um Departamento por ID
  GetDepartamento(id: number): Observable<Response<Departamento>> {
    return this.http.get<Response<Departamento>>(`${this.apiUrlD}/${id}`);
  }

  // Método para criar um novo Departamento
  PostDepartamento(departamento: Departamento): Observable<Response<Departamento[]>> {
    return this.http.post<Response<Departamento[]>>(`${this.apiUrlD}`, departamento);
  }

  // Método para atualizar um Departamento
  PutDepartamento(departamento: Departamento): Observable<Response<Departamento[]>> {
    return this.http.put<Response<Departamento[]>>(`${this.apiUrlD}`, departamento);
  }

  // Método para excluir um Departamento por ID
  DeleteDepartamento(id: number): Observable<Response<Departamento[]>> {
    return this.http.delete<Response<Departamento[]>>(`${this.apiUrlD}/${id}`);
  }

  // URL da API para operações relacionadas a Funcionários
  private apiUrlF = `${environment.ApiUrl}/Funcionario`;

  // Método para obter lista de todos os Funcionários
  GetFuncionarios(): Observable<Response<Funcionario[]>> {
    return this.http.get<Response<Funcionario[]>>(this.apiUrlF);
  }

  // Método para obter um Funcionário por ID
  GetFuncionario(id: number): Observable<Response<Funcionario>> {
    return this.http.get<Response<Funcionario>>(`${this.apiUrlF}/${id}`);
  }

  // Método para criar um novo Funcionário
  PostFuncionario(funcionario: Funcionario): Observable<Response<Funcionario[]>> {
    return this.http.post<Response<Funcionario[]>>(`${this.apiUrlF}`, funcionario);
  }

  // Método para atualizar um Funcionário
  PutFuncionario(funcionario: Funcionario): Observable<Response<Funcionario[]>> {
    return this.http.put<Response<Funcionario[]>>(`${this.apiUrlF}`, funcionario);
  }

  // Método para excluir um Funcionário por ID
  DeleteFuncionario(id: number): Observable<Response<Funcionario[]>> {
    return this.http.delete<Response<Funcionario[]>>(`${this.apiUrlF}/${id}`);
  }

  
   apiUrlI = `${environment.ApiUrl}/Imagem`;

  GetImagem(nomeDaImagem: string): Observable<any> {
    return this.http.get(`${this.apiUrlI}/${nomeDaImagem}`, { responseType: 'blob' });
  }

  PostImagem(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);
  
    const headers = new HttpHeaders({
      'enctype': 'multipart/form-data',
    });
  
    return this.http.post(`${this.apiUrlI}`, formData, { headers })
  }

}
