import { Injectable, signal } from '@angular/core';
import { PageTitle } from '../models/page-title';

@Injectable({
  providedIn: 'root',
})
export class PageTitleService {
  pageTitle = signal<PageTitle | null>(null);

  set(value: PageTitle | null) {
    this.pageTitle.set(value);
  }
}
