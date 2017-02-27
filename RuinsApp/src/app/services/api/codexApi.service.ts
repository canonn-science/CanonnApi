import {Injectable} from '@angular/core';
import {ApiBaseService} from './apiBase.service';
import {Observable} from 'rxjs/Rx';
import {Logger} from 'angular2-logger/app/core/logger';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {CodexCategoryModel} from '../../models/codexCategory';
import {CodexDataModel} from '../../models/codexDataModel';

@Injectable()
export class CodexApiService extends ApiBaseService {

	constructor(logger: Logger, http: Http, authHttp: AuthHttp) {
		super(logger, http, authHttp);
	}

	public getCodexCategoryBaseData(): Observable<CodexCategoryModel[]> {
		return this._http.get(`${this._apiBaseUrl}/v1/codex/categories`)
			.map(res => <CodexCategoryModel[]>res.json());
	}

	public getCodexDataBaseData(): Observable<CodexDataModel[]> {
		return this._http.get(`${this._apiBaseUrl}/v1/codex/data`)
			.map(res => <CodexDataModel[]>res.json());
	}
}
