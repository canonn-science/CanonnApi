import {Logger} from 'angular2-logger/app/core/logger';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';
import {RuinTypeModel} from '../../models/ruintypeModel';

@Injectable()
export class RuinTypeApiService extends ApiBasedataService<RuinTypeModel> {
	constructor(logger: Logger, http: Http, authHttp: AuthHttp) {
		super('ruins/types', logger, http, authHttp);
	}
}
