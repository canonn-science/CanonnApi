import {Logger} from 'angular2-logger/app/core/logger';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';
import {ObeliskGroupModel} from 'app/models/obeliskGroupModel';

@Injectable()
export class ObeliskGroupApiService extends ApiBasedataService<ObeliskGroupModel> {
	constructor(logger: Logger, http: Http, authHttp: AuthHttp) {
		super('obelisk/groups', logger, http, authHttp);
	}
}
