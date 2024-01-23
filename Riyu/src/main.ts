import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';

// Configuração da aplicação com o HttpClient fornecido
appConfig.providers = [...(appConfig.providers || []), provideHttpClient()];

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
