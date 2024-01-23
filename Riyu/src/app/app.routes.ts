import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { DepartamentoComponent } from './components/departamento/departamento.component';
import { FuncionarioComponent } from './components/funcionarios/funcionarios.component';


export const routes: Routes = [
    {path: '', component: HomeComponent }, //Página inicial
    {path: 'departamento', component: DepartamentoComponent }, //Página que dá acesso a tabela Departamentos do banco de dados
    {path: 'funcionarios/:nome', component: FuncionarioComponent }, //Páginas que dão acesso a tabela Funcionarios do banco de dados
];
