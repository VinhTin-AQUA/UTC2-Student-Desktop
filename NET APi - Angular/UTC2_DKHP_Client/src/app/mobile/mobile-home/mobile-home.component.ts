import { Component } from '@angular/core';
import { MobileService } from '../mobile.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MobileHubService } from '../../hubs/mobile-hub.service';
import { Notice } from '../../shared/notice';

@Component({
	selector: 'app-home',
	standalone: true,
	imports: [FormsModule],
	templateUrl: './mobile-home.component.html',
	styleUrl: './mobile-home.component.scss',
})
export class MobileHomeComponent {
	mssv: string = '';
	password: string = '';
	idHocPhans: string[] = [];
	notices: Notice[] = [];
	isDangDK: boolean = false;

	constructor(
		private mobileService: MobileService,
		private router: Router,
		private mobileHub: MobileHubService
	) {}

	ngOnInit() {
		this.mobileHub.createChatConnection();

		this.mobileHub.notices$.subscribe({
			next: res => {
				this.notices = res;
			},
		});

		// kiểm tra đăng nhập
		// this.mobileHub.loginStatus$.subscribe({
		// 	next: statusLoggedIn => {
		// 		// console.log(statusLoggedIn);
		// 		if (statusLoggedIn === false) {
		// 			this.mobileHub.resetNotice('Kiểm tra lại thông tin đăng nhập');
		// 			alert('Sai mã số sinh viên hoặc mật khẩu');
		// 		}
		// 	},
		// 	error: err => {
		// 		console.log(err);
		// 	},
		// });
	}

	ngOnDestroy() {
		this.mobileHub.stopChatConnection();
	}

	// login() {
	// 	this.mobileService.login(this.mssv, this.password).subscribe({
	// 		next: (res: any) => {
	// 			this.mssv = res.mssv;
	// 			this.password = res.password;
	// 			this.isLoggedIn = true;
	// 			alert('Đăng nhập thành công');
	// 		},
	// 		error: err => {
	// 			alert('Sai MSSV hoặc mật khẩu');
	// 		},
	// 	});
	// }

	onAddIdHocPhan(idHocPhan: any) {
		if (idHocPhan.value !== '') {
			const index = this.idHocPhans.findIndex(hp => hp === idHocPhan.value);
			if (index === -1) {
				this.idHocPhans.push(idHocPhan.value);
				idHocPhan.value = '';
			}
		}
	}

	onRemoveIdHocPhan(idHocPhan: string) {
		const index = this.idHocPhans.findIndex(id => id === idHocPhan);
		if (index !== -1) {
			this.idHocPhans.splice(index, 1);
		}
	}

	onDangKy() {
		if (this.mssv === '') {
			alert('Vui lòng nhập MÃ SỐ SINH VIÊN');
		} else if (this.password === '') {
			alert('vui lòng nhập mật khẩu');
		} else if (this.idHocPhans.length === 0) {
			alert('vui lòng nhập id học phần');
		} else {
			this.isDangDK = true;
			this.mobileHub.dangKy(this.mssv, this.password, this.idHocPhans);
		}
	}

	showStatus(id: string) {
		const notice = this.notices.find(nt => nt.idHocPhan === id);

		if (notice !== undefined) {
			return notice.message;
		}
		return 'Sẵn sàng đăng ký';
	}

	onHuyDangKy() {
		this.isDangDK = false
		this.mobileHub.huyDangKy();
	}
}
