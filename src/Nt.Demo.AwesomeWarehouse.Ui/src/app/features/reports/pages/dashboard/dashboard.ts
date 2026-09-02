import { Component, inject } from '@angular/core';
import { AsyncPipe, CurrencyPipe, DecimalPipe } from '@angular/common';
import { MatCard } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { ReportService } from '../../services/reports.dashboard';

@Component({
  imports: [
    AsyncPipe,
    CurrencyPipe,
    MatCard,
    MatButtonModule,
    MatIconModule,
    RouterLink,
    DecimalPipe,
  ],
  selector: 'app-dashboard',
  styleUrl: './dashboard.scss',
  templateUrl: './dashboard.html',
})
export class DashboardComponent {
  reportService = inject(ReportService);

  report$ = this.reportService.getReport();
}
