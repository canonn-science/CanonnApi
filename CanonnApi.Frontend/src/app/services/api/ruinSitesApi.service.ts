import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';
import {RuinSiteModel} from '../../models/ruinSiteModel';

@Injectable()
export class RuinSitesApiService extends ApiBasedataService<RuinSiteModel> {
	constructor(http: Http, authHttp: AuthHttp) {
		super('ruinsites', http, authHttp);
	}
}
