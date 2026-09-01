import { Component, inject, model } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {
  MatDialogTitle,
  MatDialogContent,
  MatDialogActions,
  MatDialogClose,
  MAT_DIALOG_DATA,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Product } from '../../model/product';

@Component({
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
  ],
  selector: 'app-increase-stock',
  styleUrl: './increase-stock.scss',
  templateUrl: './increase-stock.html',
})
export class IncreaseStock {
  readonly dialogRef = inject(MatDialogRef<IncreaseStock>);
  readonly data = inject<Product>(MAT_DIALOG_DATA);
  public increasedValue = model(0);

  onNoClick(): void {
    this.dialogRef.close();
  }
}
