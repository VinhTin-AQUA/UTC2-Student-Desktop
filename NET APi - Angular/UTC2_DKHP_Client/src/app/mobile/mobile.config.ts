import { ApplicationConfig } from "@angular/core";

import { routes } from "./mobile.routes";
import { provideRouter } from "@angular/router";

export const mobileConfige: ApplicationConfig = {
	providers: [provideRouter(routes)]
}
