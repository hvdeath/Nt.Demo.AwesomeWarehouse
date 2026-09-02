import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { finalize, Observable } from 'rxjs';
import { GetReportResponse } from '../model/report';
import { LoaderService } from '../../../core/services/loader.service';

@Injectable({ providedIn: 'root' })
export class ReportService {
  private http = inject(HttpClient);
  private baseUrl = '/api/reports/';

  loaderService = inject(LoaderService);

  getReport(): Observable<GetReportResponse> {
    this.loaderService.show();

    return this.http
      .get<GetReportResponse>(this.baseUrl)
      .pipe(finalize(() => this.loaderService.hide()));
  }
}
