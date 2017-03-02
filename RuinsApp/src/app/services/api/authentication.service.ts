import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Rx';
import {AuthHttp, tokenNotExpired} from 'angular2-jwt';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/find';
import {IClientConfiguration} from '../../models/clientConfiguration';
import {IUserInformation} from '../../models/IUserInformation';
import {ApiBaseService} from './apiBase.service';
import {Http} from '@angular/http';
import {Logger} from 'angular2-logger/app/core/logger';
import {Router} from '@angular/router';
import {LinqService} from 'ng2-linq';

@Injectable()
export class AuthenticationService extends ApiBaseService {

	private _lock: any = null;
	private _lockOptions = {
		auth: {
			redirectUrl: window.location.origin,
			responseType: 'id_token token',
			params: {
				state: '',
			}
		},
		theme: {
			logo: 'https://canonn-api.sonargaming.com/canonn.png',
		},
		socialButtonStyle: <'big' | 'small'>('small'), // huh? Strange workaround around typings in TypeScript .oO
		languageDictionary: {
			emailInputPlaceholder: 'arcanonn@canonn.science',
			title: 'Canonn Science',
		},
	};

	public userInformation: IUserInformation = null;
	private userInformation$: Observable<IUserInformation> = null;

	constructor(logger: Logger, http: Http, authHttp: AuthHttp, private _router: Router, private _linq: LinqService) {
		super(logger, http, authHttp);

		this._logger.log('[authenticationService] Instanciating & loading client configuration...');
		this.getClientConfiguration()
			.subscribe(
				(config) => this.createLockInstance(config),
				(error) => console.log(error)
			);
	}

	public login() {
		this._lockOptions.auth.params.state = this._router.url;
		this._lock.show(this._lockOptions);
	}

	public logout() {
		this.userInformation = null;
		localStorage.removeItem('id_token');
	}

	public isReady() {
		return (this._lock !== null);
	}

	public hasPermission(permission: string): boolean {
		if (this.userInformation !== null) {
			for (let i = 0; i < this.userInformation.permissions.length; i++) {
				if (this.userInformation.permissions[i] === permission) {
					return true;
				}
			}
		}

		return false;
	}

	public authenticated() {
		try {
			const result = tokenNotExpired();

			if ((result) && this.userInformation === null) {
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
		const lock = new Auth0Lock(config.clientId, config.domain, this._lockOptions);

		lock.on('authenticated', (authResult) => {
			this._logger.debug('[authenticationService] Received authentication result', authResult);
			localStorage.setItem('id_token', authResult.idToken);
			localStorage.setItem('redirectUrl', authResult.state);
			this.fetchUserInformation();
		});

		this._lock = lock;
	}

	private fetchUserInformation() {
		const url = `${this._apiBaseUrl}/v1/userinformation`;

		if (this.userInformation$ === null) {
			this.userInformation$ = this._authHttp.get(url)
				.map(res => <IUserInformation>(res.json()))
				.do(() => this.redirectIfRequired());

			this.userInformation$.subscribe(
				userInfo => {
					this._logger.debug(`[authenticationService] Received user information`, userInfo);
					this.userInformation = userInfo;
					this.userInformation$ = null;
				},
				err => this._logger.error(`[authenticationService] Error fetching user information: ${err}.`)
			);
		}
	}

	private redirectIfRequired() {
		const redirectUrl = localStorage.getItem('redirectUrl');
		if (redirectUrl) {
			localStorage.removeItem('redirectUrl');
			this._logger.debug('[authenticationService] Redirecting to previous route', redirectUrl);
			this._router.navigate([redirectUrl]);
		}
	}
}
