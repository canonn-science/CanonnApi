import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';
import {RuinSiteModel} from '../../models/ruinSiteModel';
import {Observable} from 'rxjs/Rx';

@Injectable()
export class RuinSitesApiService extends ApiBasedataService<RuinSiteModel> {
	constructor(http: Http, authHttp: AuthHttp) {
		super('ruinsites', http, authHttp);
	}

	public getForEditor(id: number): Observable<RuinSiteModel> {
		return this.http.get(this.baseUrl + `edit/${id}`)
			.map(res => <RuinSiteModel>res.json());
	}

	public saveSite(dto: RuinSiteModel): Observable<RuinSiteModel> {
		return this._authHttp.post(this.baseUrl + `edit/${dto.id}`, dto)
			.map(res => <RuinSiteModel>res.json());
	}
}
