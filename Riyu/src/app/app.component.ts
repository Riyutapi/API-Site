import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterOutlet } from '@angular/router';
import { BarraInferiorComponent } from "./components/barra_inferior/barra_inferior.component";
import { LogoComponent } from "./components/logo/logo.component";


@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [CommonModule, RouterOutlet, BarraInferiorComponent, LogoComponent, HttpClientModule]
})
export class AppComponent {
  title = 'Riyu';
}
