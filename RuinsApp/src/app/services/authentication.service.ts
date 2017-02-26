import {Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {Observable} from 'rxjs/Rx';
import {AuthHttp, tokenNotExpired} from 'angular2-jwt';
import {Logger} from 'angular2-logger/core';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/map';
import {environment} from 'environments/environment';
import {IClientConfiguration} from '../models/clientConfiguration';
import {IUserInformation} from '../models/IUserInformation';

@Injectable()
export class AuthenticationService {

	private _lock: any = null;
	private _userInfoObservable: Observable<IUserInformation> = null;
	private _options = {
		auth: {
			responseType: 'id_token token',
		}
	};

	public userInformation: IUserInformation = null;

	constructor(private _logger: Logger, private _http: Http, private _authHttp: AuthHttp) {
		this._logger.log('[authenticationService] Instanciating & loading client configuration...');
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
		localStorage.removeItem('user_information');
	}

	public isReady() {
		return (this._lock !== null);
	}

	public authenticated() {
		try {
			const result = tokenNotExpired();

			if (result) {
				this.fetchUserInformation();
			}

			return result;
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
				.do(val => this._logger.warn(`[authenticationService] Could not fetch client configuration. Error: ${val}.`))
				.delayWhen(val => Observable.timer(15000)) // retry after 15 seconds
			);
	}

	private createLockInstance(config: IClientConfiguration) {
		const lock = new Auth0Lock(config.clientId, config.domain, this._options);

		lock.on('authenticated', (authResult) => {
			this._logger.debug('[authenticationService] Received id_token', authResult.idToken);
			localStorage.setItem('id_token', authResult.idToken);
			this.fetchUserInformation();
		});

		this._lock = lock;
	}

	private fetchUserInformation() {
		if (this.userInformation !== null) {
			return;
		}

		const cachedValue = localStorage.getItem('user_information');
		if ((cachedValue !== null) && (cachedValue !== '')) {
			this.userInformation = JSON.parse(cachedValue);
		}

		if (this._userInfoObservable === null) {
			const url = `${environment.apiBaseUri}/v1/userinformation`;

			this._userInfoObservable = this._authHttp.get(url)
				.map(res => <IUserInformation>(res.json()));

			this._userInfoObservable.subscribe(
				userInfo => {
					this._logger.debug(`[authenticationService] Received user information`, userInfo);
					localStorage.setItem('user_information', JSON.stringify(userInfo));
					this.userInformation = userInfo;
					this._userInfoObservable = null;
				},
				err => this._logger.error(`[authenticationService] Error fetching user information: ${err}.`)
			);
		}
	}
}
