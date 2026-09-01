import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterLink, RouterModule } from '@angular/router';
import { MatDivider } from "@angular/material/divider";
import { MatIcon } from "@angular/material/icon";

@Component({
  imports: [RouterModule, MatToolbarModule, MatButtonModule, RouterLink, MatDivider, MatIcon],
  selector: 'app-header',
  styleUrl: './header-component.scss',
  templateUrl: './header-component.html',
})
export class HeaderComponent {
  navbarCollapse() {
    document.getElementById('navContent')?.classList.remove('show');
  }
}
