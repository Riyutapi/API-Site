import { Component } from '@angular/core';


@Component({
  selector: 'app-barra_inferior',
  standalone: true,
  imports: [],
  templateUrl: './barra_inferior.component.html',
  styleUrl: './barra_inferior.component.css'
})

export class BarraInferiorComponent {
  // Variáveis
  nome: string ='Ricardo Yudi Takahashi Pimentel';
  ano: number = 2024;
  linkedin: string = 'https://www.linkedin.com/in/ricardo-yudi-pimentel/'
  github: string = 'https://github.com/Riyutapi'
}
