import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';

@Injectable({
	providedIn: 'root',
})
export class MobileService {
	constructor(private http: HttpClient) {}

	login(mssv: string, password: string) {
		return this.http.post('https://localhost:7102/api/Mobile/login', { mssv, password });
	}

	layDSHocPhan(idMonHoc: number) {
		return this.http.get(`${environment.appUrl}/mobile/danh-sach-hoc-phan/${idMonHoc}`);
	}

	thongTinDotDK() {
		return this.http.get(`${environment.appUrl}/mobile/thong-tin-dot-dk-danh-sach-nganh-hoc`);
	}
}
