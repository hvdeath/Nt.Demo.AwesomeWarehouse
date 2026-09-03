import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
import { GetReportResponse } from '../model/report';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { pipe, switchMap } from 'rxjs';
import { tapResponse } from '@ngrx/operators';
import { ReportService } from '../services/report.service';
import { inject } from '@angular/core';
import { GlobalStore } from '../../../core/store/global.store';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackbarErrorComponent } from '../../../shared/components/snackbar-error-component/snackbar-error.component';

type ReportStoreState = {
  report: GetReportResponse;
};

const initialState: ReportStoreState = {
  report: {} as GetReportResponse,
};

export const ReportStore = signalStore(
  { providedIn: 'root' },
  withState(initialState),
  withMethods(
    (
      store,
      reportService = inject(ReportService),
      globalStore = inject(GlobalStore),
      snackBar = inject(MatSnackBar),
    ) => ({
      getReport: rxMethod<void>(
        pipe(
          switchMap(() => {
            globalStore.startLoading();
            return reportService.getReport().pipe(
              tapResponse({
                next: (report) => {
                  globalStore.stopLoading();
                  patchState(store, { report: report });
                },
                error: (err) => {
                  globalStore.stopLoading();
                  snackBar.openFromComponent(SnackbarErrorComponent, {
                    verticalPosition: 'bottom',
                    horizontalPosition: 'right',
                    panelClass: 'error-snackbar',
                    data: 'Error fetching report',
                  });
                  console.error('Error decreasing stock:', err);
                  patchState(store, { report: initialState.report });
                },
              }),
            );
          }),
        ),
      ),
    }),
  ),
);
