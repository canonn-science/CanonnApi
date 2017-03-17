import {ArtifactModel} from '../../models/artifactModel';
import {Http} from '@angular/http';
import {AuthHttp} from 'angular2-jwt';
import {ApiBasedataService} from './apiBasedata.service';
import {Injectable} from '@angular/core';

@Injectable()
export class ArtifactApiService extends ApiBasedataService<ArtifactModel> {
	constructor(http: Http, authHttp: AuthHttp) {
		super('artifacts', http, authHttp);
	}
}
