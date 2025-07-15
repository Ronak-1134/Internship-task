import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { DemoComponent } from './demo/demo.component';
import { DirectivesComponent } from './directives/directives.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [DemoComponent, DirectivesComponent, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  collegeName = 'LDCE';
  isDisabled = false;

  onClick() {
    console.log('Button Clicked');
  }
}
