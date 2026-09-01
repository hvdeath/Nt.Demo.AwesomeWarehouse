import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GetReportResponse } from '../model/report';

@Injectable({ providedIn: 'root' })
export class ReportService {
  private http = inject(HttpClient);
  private baseUrl = '/api/reports/';

  getReport(): Observable<GetReportResponse> {
    return this.http.get<GetReportResponse>(this.baseUrl);
  }
}
