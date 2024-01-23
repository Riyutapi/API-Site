import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, NavigationEnd } from '@angular/router';
import { Location } from '@angular/common';


@Component({
  selector: 'app-logo',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './logo.component.html',
  styleUrl: './logo.component.css'
})

export class LogoComponent {
  urlAtual = '';

  // Construtor que recebe instâncias do Router e Location ao ser instanciado.
  constructor(private router: Router, private location: Location) {
    // Assina eventos de navegação para atualizar a URL atual quando a navegação é concluída.
    this.router.events.subscribe((evento) => {
      if (evento instanceof NavigationEnd) {
        // Atualiza a URL atual apenas quando a navegação é concluída.
        this.urlAtual = evento.urlAfterRedirects;
      }
    });
  }

  // Método para voltar à página anterior, se houver um estado de navegação.
  voltar() {
    if (this.location.getState() !== undefined) {
      // Utiliza o serviço Location para navegar de volta apenas se houver um estado de navegação.
      this.location.back();
    }
  }
}
