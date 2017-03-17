import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';
import {BodyModel} from '../../models/bodyModel';

@Injectable()
export class BodyApiService extends ApiBasedataService<BodyModel> {
	constructor(http: Http, authHttp: AuthHttp) {
		super('stellar/bodies', http, authHttp);
	}
}
