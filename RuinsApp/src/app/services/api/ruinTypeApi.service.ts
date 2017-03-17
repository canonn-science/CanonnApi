import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';
import {RuinTypeModel} from '../../models/ruintypeModel';

@Injectable()
export class RuinTypeApiService extends ApiBasedataService<RuinTypeModel> {
	constructor(http: Http, authHttp: AuthHttp) {
		super('ruins/types', http, authHttp);
	}
}
