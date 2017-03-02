import {Injectable} from '@angular/core';
import {ApiBaseService} from './apiBase.service';
import {Observable} from 'rxjs/Rx';
import {RelictsModel} from '../../models/relictsModel';
import {Logger} from 'angular2-logger/app/core/logger';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';

@Injectable()
export class RelictsApiService extends ApiBaseService {

	private _baseUrl = null;

	constructor(logger: Logger, http: Http, authHttp: AuthHttp) {
		super(logger, http, authHttp);

		this._baseUrl = `${this._apiBaseUrl}/v1/relicts/`;
	}

	public getRelicsBaseData(): Observable<RelictsModel[]> {
		return this._http.get(this._baseUrl)
			.map(res => <RelictsModel[]>res.json());
	}

	public saveOrUpdate(relict: RelictsModel): Observable<RelictsModel> {
		if (relict.id === 0) {
			return this.createNew(relict);
		} else {
			return this.saveChanges(relict);
		}
	}

	private createNew(relict: RelictsModel): Observable<RelictsModel> {
		return this._authHttp.post(this._baseUrl, relict)
			.map(res => <RelictsModel>res.json());
	}

	private saveChanges(relict: RelictsModel): Observable<RelictsModel> {
		return this._authHttp.put(this._baseUrl + relict.id, relict)
			.map(res => <RelictsModel>res.json());
	}

	public delete(relictId: number): Observable<any> {
		return this._authHttp.delete(this._baseUrl + relictId);
	}
}
