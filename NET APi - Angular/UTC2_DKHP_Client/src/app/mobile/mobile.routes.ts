import { Routes } from '@angular/router';
import { MobileHomeComponent } from './mobile-home/mobile-home.component';
import { MobileComponent } from './mobile.component';
import { GetIdHocPhanComponent } from './get-id-hoc-phan/get-id-hoc-phan.component';

export const routes: Routes = [
	{
		path: '', component: MobileComponent,
		children: [
			{path: '', component: MobileHomeComponent, title: 'mobile home'},
			{path: 'lay-id-hoc-phan', component: GetIdHocPhanComponent, title: 'lấy id học phần'},
		]
	},
];