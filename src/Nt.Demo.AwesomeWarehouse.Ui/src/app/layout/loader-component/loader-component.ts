import { Component } from '@angular/core';
import { MatProgressSpinner } from "@angular/material/progress-spinner";

@Component({
  imports: [MatProgressSpinner],
  selector: 'app-loader',
  styleUrl: './loader-component.scss',
  templateUrl: './loader-component.html',
})
export class LoaderComponent {}
