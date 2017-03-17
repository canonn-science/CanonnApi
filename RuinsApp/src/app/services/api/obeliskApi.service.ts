import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';
import {ObeliskModel} from 'app/models/obeliskModel';
import {Observable} from 'rxjs/Rx';

@Injectable()
export class ObeliskApiService extends ApiBasedataService<ObeliskModel> {
	constructor(http: Http, authHttp: AuthHttp) {
		super('obelisks', http, authHttp);
	}

	public search(ruintypeId: number = 0, obeliskgroupId: number = 0): Observable<ObeliskModel> {
		return this._http.get(`${this.baseUrl}/search?ruintypeId=${ruintypeId}&obeliskgroupId=${obeliskgroupId}`)
			.map(res => <ObeliskModel>res.json());
	}
}
