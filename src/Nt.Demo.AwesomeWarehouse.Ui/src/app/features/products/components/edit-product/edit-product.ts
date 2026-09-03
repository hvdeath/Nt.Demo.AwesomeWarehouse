import { Component, effect, inject, input, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product } from '../../model/product';
import { ProductStore } from '../../store/product.store';
import { GlobalStore } from '../../../../core/store/global.store';

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
  private productStore = inject(ProductStore);
  globalStore = inject(GlobalStore);

  storedProduct?: Product = undefined;

  constructor() {
    effect(() => {
      const product = this.productStore.selectedProduct();

      if (product) {
        this.storedProduct = product;
        this.productForm.setValue({
          name: product.name,
          description: product.description,
          weight: product.weight,
          unitPrice: product.unitPrice,
          quantity: product.quantity,
        });
      }
    });
  }

  ngOnInit() {
    if (this.productId()) {
      this.productStore.getProduct(this.productId()!);
    }
  }

  cancel() {
    this.productStore.clearSelectedProduct();
    this.router.navigate(['/products']);
  }

  onSubmit() {
    if (this.productForm.valid) {
      const value = this.productForm.value;

      if (this.productId()) {
        this.productStore.updateProduct({
          id: this.productId()!,
          product: {
            name: value.name!,
            description: value.description!,
            unitPrice: value.unitPrice!,
            weight: value.weight!,
            quantity: value.quantity!,
            version: this.storedProduct?.version!,
          },
        });
      } else {
        this.productStore.createProduct({
          product: {
            name: value.name!,
            description: value.description!,
            unitPrice: value.unitPrice!,
            weight: value.weight!,
            quantity: value.quantity!,
          },
        });
      }
    }
  }
}
