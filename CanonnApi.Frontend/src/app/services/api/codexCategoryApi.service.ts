import {Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {CodexCategoryModel} from '../../models/codexCategoryModel';
import {ApiBasedataService} from './apiBasedata.service';

@Injectable()
export class CodexCategoryApiService extends ApiBasedataService<CodexCategoryModel> {

	constructor(http: Http, authHttp: AuthHttp) {
		super('codex/categories', http, authHttp);
	}
}
