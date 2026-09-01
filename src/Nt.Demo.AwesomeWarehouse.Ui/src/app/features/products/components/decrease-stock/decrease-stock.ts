import { Component, inject, model } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { IncreaseStock } from '../increase-stock/increase-stock';
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
  selector: 'app-decrease-stock',
  styleUrl: './decrease-stock.scss',
  templateUrl: './decrease-stock.html',
})
export class DecreaseStock {
  readonly dialogRef = inject(MatDialogRef<IncreaseStock>);
  readonly data = inject<Product>(MAT_DIALOG_DATA);
  public increasedValue = model(0);

  onNoClick(): void {
    this.dialogRef.close();
  }
}
