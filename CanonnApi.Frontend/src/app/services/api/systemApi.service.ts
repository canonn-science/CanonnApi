import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';
import {SystemModel} from '../../models/systemModel';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class SystemApiService extends ApiBasedataService<SystemModel> {
	constructor(http: Http, authHttp: AuthHttp) {
		super('stellar/systems', http, authHttp);
	}

	public fetchEdsmIds(): Observable<any> {
		return this.authHttp.get(this.baseUrl + 'updateEdsmIds');
	}
}
