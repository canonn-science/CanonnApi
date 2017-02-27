import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Rx';
import {AuthHttp, tokenNotExpired} from 'angular2-jwt';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/map';
import {IClientConfiguration} from '../../models/clientConfiguration';
import {IUserInformation} from '../../models/IUserInformation';
import {ApiBaseService} from './apiBase.service';
import {Http} from '@angular/http';
import {Logger} from 'angular2-logger/app/core/logger';
import {Router} from '@angular/router';

@Injectable()
export class AuthenticationService extends ApiBaseService {

	private _lock: any = null;
	private _userInfoObservable: Observable<IUserInformation> = null;
	private _options = {
		auth: {
			redirectUrl: window.location.origin,
			responseType: 'id_token token',
			params: {
				state: '',
			}
		}
	};

	public userInformation: IUserInformation = null;

	constructor(logger: Logger, http: Http, authHttp: AuthHttp, private _router: Router) {
		super(logger, http, authHttp);

		this._logger.log('[authenticationService] Instanciating & loading client configuration...');
		this.getClientConfiguration()
			.subscribe(
				(config) => this.createLockInstance(config),
				(error) => console.log(error)
			);
	}

	public login() {
		this._options.auth.params.state = this._router.url;
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
		const url = `${this._apiBaseUrl}/v1/clientconfiguration`;
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
			this._logger.debug('[authenticationService] Received authentication result', authResult);
			localStorage.setItem('id_token', authResult.idToken);
			localStorage.setItem('redirectUrl', authResult.state);
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
			const url = `${this._apiBaseUrl}/v1/userinformation`;

			this._userInfoObservable = this._authHttp.get(url)
				.map(res => <IUserInformation>(res.json()))
				.do(() => {
					const redirectUrl = localStorage.getItem('redirectUrl');
					if (redirectUrl) {
						localStorage.removeItem('redirectUrl');
						this._logger.debug('[authenticationService] Redirecting to previous route', redirectUrl);
						this._router.navigate([redirectUrl]);
					}
				});

			this._userInfoObservable.subscribe(
				userInfo => {
					this._logger.debug(`[authenticationService] Received user information`, userInfo);
					localStorage.setItem('user_information', JSON.stringify(userInfo));
					this.userInformation = userInfo;
					this._userInfoObservable = null;
				},
				err => this._logger.error(`[authenticationService] Error fetching user information: ${err}.`)
			);

			return this._userInfoObservable;
		}
	}
}
