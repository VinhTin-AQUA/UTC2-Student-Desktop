import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from '../../environments/environment.development';
import { Notice } from '../shared/notice';
import { BehaviorSubject } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class MobileHubService {
	private connection?: HubConnection;

	private noticesSubject = new BehaviorSubject<Notice[]>([]);
	notices$ = this.noticesSubject.asObservable();
	private loginStatusSubject = new BehaviorSubject<boolean>(true);
	loginStatus$ = this.loginStatusSubject.asObservable();

	constructor() {}

	createChatConnection() {
		this.connection = new HubConnectionBuilder()
			.withUrl(`${environment.mobileHubUrl}/hubs/dkhp`)
			.withAutomaticReconnect()
			.build();

		this.connection.start().catch(error => {
			console.log(error);
		});

		this.connection.on('ReceiveMessage', (notice: any) => {
			// console.log(notice);
			if (notice !== null) {
				this.updateNotices(notice);
			}
		});

		this.connection.on('LoggedInFail', (status: boolean) => {
			// console.log(notice);
			this.loginStatusSubject.next(status);
		});

		this.connection.on('HuyDangKy', (message) => {
			console.log(123);
			
			this.resetNotice('Sẵn sàng đăng ký')
		});
	}

	stopChatConnection() {
		this.connection?.stop().catch(error => {
			console.log(error);
		});
	}

	dangKy(mssv: string, password: string, ids: string[]) {
		let notices: Notice[] = [];
		for (let id of ids) {
			notices.push({
				idHocPhan: id,
				status: false,
				message: 'Đang đăng ký. Xin vui lòng chờ.',
			});
		}
		this.noticesSubject.next(notices);
		this.connection?.invoke('DangKy', mssv, password, ids).catch(err => {
			console.log(err);
		});
	}

	resetNotice(message: string) {
		const currentNotice = this.noticesSubject.value.slice();
		for (let notice of currentNotice) {
			notice.message = message
		}
		this.noticesSubject.next(currentNotice);
	}

	huyDangKy() {

	}

	/* private methods */
	private updateNotices(newNotice: Notice) {
		// Tạo một bản sao của mảng
		const currentNotice = this.noticesSubject.value.slice();

		let notice = currentNotice.find(nt => nt.idHocPhan === newNotice.idHocPhan);

		if (notice !== null && notice !== undefined) {
			notice.message = newNotice.message;
			notice.status = newNotice.status;
			this.noticesSubject.next(currentNotice);
		}
	}
}
