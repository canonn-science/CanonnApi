import {Injectable} from '@angular/core';
import {AuthHttp, tokenNotExpired} from 'angular2-jwt';
import {Http} from '@angular/http';
import {Observable} from 'rxjs/Rx';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/map';
import {environment} from 'environments/environment';
import {Logger} from 'angular2-logger/core';
import {IClientConfiguration} from '../models/clientConfiguration';

@Injectable()
export class AuthenticationService {

	private _lock: any = null;
	private _options = {
		auth: {
			responseType: 'id_token token',
		}
	};

	constructor(private _logger: Logger, private _http: Http, private _authHttp: AuthHttp) {
		this._logger.log('Instanciating AuthenticationService. Loading client configuration...');
		this.getClientConfiguration()
			.subscribe(
				(config) => this.createLockInstance(config),
				(error) => console.log(error)
			);
	}

	public login() {
		this._lock.show(this._options);
	}

	public logout() {
		localStorage.removeItem('id_token');
	}

	public isReady() {
		return (this._lock !== null);
	}

	public authenticated() {
		try {
			return tokenNotExpired();
		} catch (e) {
			this.logout();
		}
	}

	private getClientConfiguration(): Observable<IClientConfiguration> {
		const url = `${environment.apiBaseUri}/v1/clientconfiguration`;
		this._logger.debug('[authenticationService] trying to load client configuration from url', url);

		return this._http.get(url)
			.map((res) => {
				const obj = res.json();
				this._logger.debug('[authenticationService] Received client configuration', obj);
				return <IClientConfiguration>(obj);
			})
			.retryWhen(err => err
				.do(val => this._logger.warn(`Could not fetch client configuration. Error: ${val}.`))
				.delayWhen(val => Observable.timer(15000)) // retry after 15 seconds
			);
	}

	private createLockInstance(config: IClientConfiguration) {
		const lock = new Auth0Lock(config.clientId, config.domain, this._options);

		lock.on('authenticated', (authResult) => {
			this._logger.debug('[authenticationService] Received id_token', authResult.idToken);
			localStorage.setItem('id_token', authResult.idToken);
		});

		this._lock = lock;
	}
}
