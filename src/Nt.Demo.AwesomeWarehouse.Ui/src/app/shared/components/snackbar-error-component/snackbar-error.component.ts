import { Component, OnInit, Inject } from '@angular/core';
import { MatSnackBarRef, MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';
import { MatIcon } from "@angular/material/icon";
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-snackbar-error',
  templateUrl: './snackbar-error.component.html',
  styleUrls: ['./snackbar-error.component.scss'],
  imports: [MatIcon, MatButtonModule]
})
export class SnackbarErrorComponent implements OnInit {

  constructor(private matDialogRef: MatSnackBarRef<SnackbarErrorComponent>, @Inject(MAT_SNACK_BAR_DATA) public message: any) {
  }

  ngOnInit() {}

  close() {
    this.matDialogRef.dismiss();
  }

}