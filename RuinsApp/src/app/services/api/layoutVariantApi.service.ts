import {Logger} from 'angular2-logger/app/core/logger';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';
import {RuinlayoutVariantModel} from '../../models/ruinlayoutVariantModel';

@Injectable()
export class LayoutVariantApiService extends ApiBasedataService<RuinlayoutVariantModel> {
	constructor(logger: Logger, http: Http, authHttp: AuthHttp) {
		super('layouts/variants', logger, http, authHttp);
	}
}
