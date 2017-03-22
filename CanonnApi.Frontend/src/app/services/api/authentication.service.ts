import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Rx';
import {AuthHttp, tokenNotExpired} from 'angular2-jwt';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/find';
import {ClientConfiguration} from '../../models/clientConfiguration';
import {UserInformation} from '../../models/userInformation';
import {ApiBaseService} from './apiBase.service';
import {Http} from '@angular/http';
import {Router} from '@angular/router';

@Injectable()
export class AuthenticationService extends ApiBaseService {

	private _lock: any;
	private _lockOptions = {
		auth: {
			redirectUrl: window.location.origin,
			responseType: 'id_token token',
			params: {
				state: '',
			}
		},
		theme: {
			logo: '/assets/canonn.png',
		},
		socialButtonStyle: <'big' | 'small'>('small'), // huh? Strange workaround around typings in TypeScript .oO
		languageDictionary: {
			emailInputPlaceholder: 'arcanonn@canonn.science',
			title: 'Canonn Science',
		},
	};

	public userInformation: UserInformation;
	private userInformation$: Observable<UserInformation>;

	constructor(http: Http, authHttp: AuthHttp, private _router: Router) {
		super(http, authHttp);

		this.getClientConfiguration()
			.subscribe(
				(config) => this.createLockInstance(config),
				(error) => console.log(error)
			);
	}

	public login() {
		this._lockOptions.auth.params.state = this._router.url;
		this._lock.show(this._lockOptions);
		return false;
	}

	public logout() {
		this.userInformation = void 0;
		localStorage.removeItem('id_token');
		return false;
	}

	public isReady() {
		return (this._lock);
	}

	public hasPermission(permission: string): boolean {
		if (this.userInformation) {
			return (this.userInformation.permissions.includes(permission));
		}

		return false;
	}

	public authenticated() {
		try {
			const result = tokenNotExpired();

			if (result && !this.userInformation) {
				this.fetchUserInformation();
			}

			return result;
		} catch (e) {
			this.logout();
		}
	}

	private getClientConfiguration(): Observable<ClientConfiguration> {
		const url = `${this._apiBaseUrl}/v1/clientconfiguration`;

		return this._http.get(url)
			.map((res) => {
				const obj = res.json();
				return <ClientConfiguration>(obj);
			})
			.retryWhen(err => err
				.delayWhen(val => Observable.timer(15000)) // retry after 15 seconds
			);
	}

	private createLockInstance(config: ClientConfiguration) {
		const lock = new Auth0Lock(config.clientId, config.domain, this._lockOptions);

		lock.on('authenticated', (authResult) => {
			localStorage.setItem('id_token', authResult.idToken);
			localStorage.setItem('redirectUrl', authResult.state);
			this.fetchUserInformation();
		});

		this._lock = lock;
	}

	private fetchUserInformation() {
		const url = `${this._apiBaseUrl}/v1/userinformation`;

		if (!this.userInformation$) {
			this.userInformation$ = this._authHttp.get(url)
				.map(res => <UserInformation>(res.json()))
				.do(() => this.redirectIfRequired());

			this.userInformation$.subscribe(
				userInfo => {
					this.userInformation = userInfo;
					this.userInformation$ = void 0;
				},
			);
		}
	}

	private redirectIfRequired() {
		const redirectUrl = localStorage.getItem('redirectUrl');
		if (redirectUrl) {
			localStorage.removeItem('redirectUrl');
			this._router.navigate([redirectUrl]);
		}
	}
}