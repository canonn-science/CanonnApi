import {Injectable} from '@angular/core';
import {Logger} from 'angular2-logger/app/core/logger';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {CodexCategoryModel} from '../../models/codexCategoryModel';
import {ApiBasedataService} from './apiBasedata.service';

@Injectable()
export class CodexCategoryApiService extends ApiBasedataService<CodexCategoryModel> {

	constructor(logger: Logger, http: Http, authHttp: AuthHttp) {
		super('codex/categories', logger, http, authHttp);
	}
}
