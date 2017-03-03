import {Injectable} from '@angular/core';
import {Logger} from 'angular2-logger/app/core/logger';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {CodexDataModel} from '../../models/codexDataModel';
import {ApiBasedataService} from './apiBasedata.service';

@Injectable()
export class CodexDataApiService extends ApiBasedataService<CodexDataModel> {

	constructor(logger: Logger, http: Http, authHttp: AuthHttp) {
		super('codex/data', logger, http, authHttp);
	}
}
