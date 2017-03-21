import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';
import {ObeliskGroupModel} from 'app/models/obeliskGroupModel';

@Injectable()
export class ObeliskGroupApiService extends ApiBasedataService<ObeliskGroupModel> {
	constructor(http: Http, authHttp: AuthHttp) {
		super('obelisks/groups', http, authHttp);
	}
}
