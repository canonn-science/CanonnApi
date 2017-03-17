import {AuthHttp} from 'angular2-jwt';
import {Http} from '@angular/http';
import {environment} from '../../../environments/environment';

export class ApiBaseService {
	protected _apiBaseUrl: string = environment.apiBaseUri;

	constructor(protected _http: Http, protected _authHttp: AuthHttp) {
	}
}
