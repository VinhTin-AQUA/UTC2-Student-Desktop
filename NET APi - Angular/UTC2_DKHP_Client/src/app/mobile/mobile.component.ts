import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { SidebarComponent } from "./components/sidebar/sidebar.component";

@Component({
	selector: "app-mobile",
	standalone: true,
	imports: [RouterOutlet, SidebarComponent],
	templateUrl: "./mobile.component.html",
	styleUrl: "./mobile.component.scss",
})
export class MobileComponent {}
