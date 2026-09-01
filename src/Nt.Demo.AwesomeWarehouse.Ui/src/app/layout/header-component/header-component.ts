import { Component } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';

@Component({
  imports: [RouterModule, MatToolbarModule, MatButtonModule, MatIconModule, MatMenuModule, MatBadgeModule],
  selector: 'app-header',
  styleUrl: './header-component.scss',
  templateUrl: './header-component.html',
})
export class HeaderComponent {

  navbarCollapse() {
    document.getElementById('navContent')?.classList.remove('show');
  }
}
