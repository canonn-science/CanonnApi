import {RelictsModel} from '../../models/relictsModel';
import {Logger} from 'angular2-logger/app/core/logger';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';

@Injectable()
export class RelictsApiService extends ApiBasedataService<RelictsModel> {

	private _baseUrl = null;

	constructor(logger: Logger, http: Http, authHttp: AuthHttp) {
		super('relicts', logger, http, authHttp);

		this._baseUrl = `${this._apiBaseUrl}/v1/relicts/`;
	}
}
