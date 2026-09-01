import { Component, DestroyRef, inject, input, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Product } from '../../model/product';

@Component({
  imports: [MatFormFieldModule, MatInputModule, ReactiveFormsModule, MatButton],
  selector: 'app-edit-product',
  styleUrl: './edit-product.scss',
  templateUrl: './edit-product.html',
})
export class EditProductComponent implements OnInit {
  public productForm = new FormGroup({
    name: new FormControl('', {
      validators: [Validators.required],
    }),
    description: new FormControl('', {
      validators: [],
    }),
    unitPrice: new FormControl(0, {
      validators: [Validators.required, Validators.min(0)],
    }),
    weight: new FormControl(0, {
      validators: [Validators.required, Validators.min(0)],
    }),
    quantity: new FormControl(0, {
      validators: [Validators.required, Validators.min(0)],
    }),
  });

  productId = input<number>();

  router = inject(Router);
  productService = inject(ProductService);
  private snackBar = inject(MatSnackBar);
  private destroyRef = inject(DestroyRef);

  storedProduct? : Product = undefined;

  ngOnInit() {
    if (this.productId()) {
      this.productService
        .getProduct(this.productId()!)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (product) => {
            this.storedProduct = product;
            this.productForm.setValue({
              name: product.name,
              description: product.description,
              weight: product.weight,
              unitPrice: product.unitPrice,
              quantity: product.quantity,
            });
          },
          error: () => this.snackBar.open('Saving failed!'),
        });
    }
  }

  goBack() {
    this.router.navigate(['/products']);
  }

  onSubmit() {
    console.log(this.productForm);
    if (this.productForm.valid) {
      const value = this.productForm.value;

      if (this.productId()) {
        this.productService
          .updateProduct(this.productId()!, {
            name: value.name!,
            description: value.description!,
            unitPrice: value.unitPrice!,
            weight: value.weight!,
            quantity: value.quantity!,
            version: this.storedProduct?.version!
          })
          .pipe(takeUntilDestroyed(this.destroyRef))
          .subscribe({
            next: () => {
              this.snackBar.open('Updated successfully!');
              this.goBack();
            },
            error: () => this.snackBar.open('Saving failed!'),
          });
      } else {
        this.productService
          .createProduct({
            name: value.name!,
            description: value.description!,
            unitPrice: value.unitPrice!,
            weight: value.weight!,
            quantity: value.quantity!
          })
          .pipe(takeUntilDestroyed(this.destroyRef))
          .subscribe({
            next: () => {
              this.snackBar.open('Created successfully!');
              this.goBack();
            },
            error: () => this.snackBar.open('Saving failed!'),
          });
      }
    }
  }
}
