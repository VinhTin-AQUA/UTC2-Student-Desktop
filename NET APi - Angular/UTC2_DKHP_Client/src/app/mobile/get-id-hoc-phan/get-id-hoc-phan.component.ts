import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MobileService } from '../mobile.service';
import { Router } from '@angular/router';
import { MobileHubService } from '../../hubs/mobile-hub.service';

@Component({
	selector: 'app-get-id-hoc-phan',
	standalone: true,
	imports: [FormsModule],
	templateUrl: './get-id-hoc-phan.component.html',
	styleUrl: './get-id-hoc-phan.component.scss',
})
export class GetIdHocPhanComponent {
	dsHocPhans: any[] = [];
	mssv: string = '';
	password: string = '';
	dsMonHoc: any[] = [];
	iddot: number = -1;
	isLoggedIn: boolean = false;

	constructor(
		private mobileService: MobileService, 
		private router: Router,
		private mobileHub: MobileHubService) {}

	ngOnInit() {
		// this.mobileService.login(this.mssv, this.password).subscribe({
		// 	next: (res: any) => {
		// 		(this.mssv = res.mssv), (this.password = res.password);
		// 		this.isLoggedIn = true;
		// 		this.getThongTinLopHocPhan();
		// 	},
		// 	error: (err) => {
		// 		console.log();
		// 	}
		// });
	}

	check() {
		// console.log(this.mssv);
		// console.log(this.password);
	}

	login() {
		if (this.mssv === '') {
			alert('Vui lòng nhập mã số sinh viên');
			return;
		}

		if (this.password === '') {
			alert('Vui lòng nhập mật khẩu');
			return;
		}

		this.mobileService.login(this.mssv, this.password).subscribe({
			next: (res: any) => {
				(this.mssv = res.mssv), (this.password = res.password);
				this.isLoggedIn = true;
				alert('Đăng nhập thành công');
				this.getThongTinLopHocPhan();
			},
			error: err => {
				alert('Sai MSSV hoặc mật khẩu');
			},
		});
	}

	getThongTinLopHocPhan() {
		if (this.isLoggedIn === false) {
			alert('Bạn chưa đăng nhập');
			return;
		}

		this.mobileService.login(this.mssv, this.password).subscribe({
			next: (res: any) => {
				this.getThongTinDotDK();
			},
			error: err => {
				console.log(err);
			},
		});
	}

	loadDSHocPhan(idMonHoc: string) {
		this.mobileService.layDSHocPhan(parseInt(idMonHoc)).subscribe({
			next: (res: any) => {
				// console.log(res.thongTinDotHocPhan.hocphan_ct);
				this.dsHocPhans = res.thongTinDotHocPhan.hocphan_ct;

				for (let item of this.dsHocPhans) {
					item.tkb = item.tkb.replace('&rarr;', '➔');
					item.tkb = item.tkb.replace('<br>', '');
				}
			},
			error: err => {
				//console.log(err);
				if (err.error.status_code === 401) {
					this.router.navigateByUrl('/');
				}
			},
		});
	}

	/* private method */
	private getThongTinDotDK() {
		this.mobileService.thongTinDotDK().subscribe({
			next: (res: any) => {
				// console.log(res);
				this.dsMonHoc = res.content.monhoccombox; //(iD_MONHOC,teN_MONHOC)
				this.iddot = res.content.dotHocPhans[0].iddot;

				if (this.dsMonHoc.length > 0) {
					this.loadDSHocPhan(this.dsMonHoc[0].iD_MONHOC);
				}
			},
			error: err => {
				console.log(err);
			},
		});
	}
}
