import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDivider } from '@angular/material/divider';
import { PageTitleService } from '../../core/services/page-title.service';

@Component({
  imports: [MatDivider, MatButtonModule],
  selector: 'app-page-title',
  styleUrl: './page-title-component.scss',
  templateUrl: './page-title-component.html',
})
export class PageTitleComponent {
  pageTitleService = inject(PageTitleService);
  
  get pageTitle() {
    return this.pageTitleService.pageTitle;
  }

  getCleanTitle(title?: string) {
    return title?.split(' - ')[0];
  }
}
