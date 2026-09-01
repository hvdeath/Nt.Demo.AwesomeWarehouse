import { Component } from '@angular/core';
import { PageTitleComponent } from "../page-title-component/page-title-component";
import { HeaderComponent } from "../header-component/header-component";
import { RouterOutlet } from "@angular/router";

@Component({
  imports: [PageTitleComponent, HeaderComponent, RouterOutlet],
  selector: 'app-shell',
  styleUrl: './shell-component.scss',
  templateUrl: './shell-component.html',
})
export class ShellComponent {}
