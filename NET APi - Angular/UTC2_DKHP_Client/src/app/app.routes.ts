import { Routes } from '@angular/router';
import { MobileComponent } from './Mobile/mobile.component';
import { HomeComponent } from './home/home.component';
import { ChooseVersionComponent } from './choose-version/choose-version.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, title: '' },
  { path: 'choose-version', component: ChooseVersionComponent, title: 'Chọn nền tảng' },
  { path: 'mobile', loadChildren: () => import('./Mobile/mobile.routes').then((m) => m.routes) },
];
