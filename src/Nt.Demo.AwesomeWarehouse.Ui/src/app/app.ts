import { Component, inject, signal } from '@angular/core';
import {
  Event as RouterEvent,
  NavigationEnd,
  Router,
  RouterOutlet,
} from '@angular/router';
import { LoaderComponent } from './layout/loader-component/loader-component';
import { LoaderService } from './core/services/loader.service';
import { PageTitleService } from './core/services/page-title.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, LoaderComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  protected readonly title = signal('Nt.Demo.AwesomeWarehouse.Ui');

  loadingService = inject(LoaderService);
  router = inject(Router);
  pageTitleService = inject(PageTitleService);

  isLoading = this.loadingService.loader;
  constructor() {
    this.router.events.subscribe((event) => this.navigationInterceptor(event));
  }

  get loader() {
    return this.loadingService.loader;
  }

  private navigationInterceptor(event: RouterEvent): void {
    if (event instanceof NavigationEnd) {
      this.updatePageTitle();
    }
  }

  private updatePageTitle(): void {
    let activeRoute = this.router.routerState.root;

    while (activeRoute.firstChild) {
      activeRoute = activeRoute.firstChild;
    }

    const title = activeRoute.snapshot.title;
    this.pageTitleService.set(title ? { title } : null);
  }
}
