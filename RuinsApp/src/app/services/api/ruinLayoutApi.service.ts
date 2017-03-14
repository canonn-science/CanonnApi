import {Logger} from 'angular2-logger/app/core/logger';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';
import {RuinLayoutModel} from '../../models/ruinLayoutModel';

@Injectable()
export class RuinLayoutApiService extends ApiBasedataService<RuinLayoutModel> {
	constructor(logger: Logger, http: Http, authHttp: AuthHttp) {
		super('ruins/layouts', logger, http, authHttp);
	}
}
