import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDivider } from '@angular/material/divider';

@Component({
  imports: [MatDivider, MatButtonModule],
  selector: 'app-page-title',
  styleUrl: './page-title-component.scss',
  templateUrl: './page-title-component.html',
})
export class PageTitleComponent {}
