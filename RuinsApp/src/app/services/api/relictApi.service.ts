import {RelictModel} from '../../models/relictModel';
import {Logger} from 'angular2-logger/app/core/logger';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';

@Injectable()
export class RelictApiService extends ApiBasedataService<RelictModel> {
	constructor(logger: Logger, http: Http, authHttp: AuthHttp) {
		super('relicts', logger, http, authHttp);
	}
}
