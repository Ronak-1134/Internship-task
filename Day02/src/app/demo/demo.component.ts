import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DemoService } from '../services/demo.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-demo',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './demo.component.html',
  styleUrl: './demo.component.css',
})
export class DemoComponent {
  name = '';

  constructor(private demoService: DemoService) {}

  ngOnInit() {
    this.demoService.fetchData();
  }
}
