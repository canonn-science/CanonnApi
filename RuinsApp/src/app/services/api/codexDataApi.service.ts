import {Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {CodexDataModel} from '../../models/codexDataModel';
import {ApiBasedataService} from './apiBasedata.service';

@Injectable()
export class CodexDataApiService extends ApiBasedataService<CodexDataModel> {

	constructor(http: Http, authHttp: AuthHttp) {
		super('codex/data', http, authHttp);
	}
}
