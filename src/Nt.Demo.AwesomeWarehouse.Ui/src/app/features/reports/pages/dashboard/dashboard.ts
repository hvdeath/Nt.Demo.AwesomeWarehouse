import { Component, inject, OnInit } from '@angular/core';
import { CurrencyPipe, DecimalPipe } from '@angular/common';
import { MatCard } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { ReportService } from '../../services/report.service';
import { ReportStore } from '../../store/report-store';

@Component({
  imports: [
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
export class DashboardComponent implements OnInit {
  
  reportService = inject(ReportService);
  reportStore = inject(ReportStore);

  ngOnInit(): void {
    this.reportStore.getReport();
  }  
}
