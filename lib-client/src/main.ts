import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import '@angular/compiler';

bootstrapApplication(AppComponent, appConfig)
  .catch(() => {
    // Обработка ошибки инициализации приложения
  });
