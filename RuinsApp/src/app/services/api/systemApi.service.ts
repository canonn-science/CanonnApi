import {Logger} from 'angular2-logger/app/core/logger';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';
import {SystemModel} from '../../models/systemModel';

@Injectable()
export class SystemApiService extends ApiBasedataService<SystemModel> {
	constructor(logger: Logger, http: Http, authHttp: AuthHttp) {
		super('stellar/systems', logger, http, authHttp);
	}
}
