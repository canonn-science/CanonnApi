import {Injectable} from '@angular/core';
import {ApiBaseService} from './apiBase.service';
import {Observable} from 'rxjs/Rx';
import {RelictsModel} from '../../models/relictsModel';
import {Logger} from 'angular2-logger/core';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';

@Injectable()
export class RelictsApiService extends ApiBaseService {

	constructor(logger: Logger, http: Http, authHttp: AuthHttp) {
		super(logger, http, authHttp);
	}

	public getRelicsBaseData(): Observable<RelictsModel[]> {
		return this._http.get(`${this._apiBaseUrl}/v1/relicts`)
			.map(res => <RelictsModel[]>res.json());
	}
}
