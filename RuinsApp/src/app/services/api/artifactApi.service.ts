import {ArtifactModel} from '../../models/artifactModel';
import {Logger} from 'angular2-logger/app/core/logger';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';

@Injectable()
export class ArtifactApiService extends ApiBasedataService<ArtifactModel> {
	constructor(logger: Logger, http: Http, authHttp: AuthHttp) {
		super('artifacts', logger, http, authHttp);
	}
}
