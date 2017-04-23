import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Rx';
import {AuthHttp, tokenNotExpired} from 'angular2-jwt';
import {ClientConfiguration} from '../../models/clientConfiguration';
import {UserInformation} from '../../models/userInformation';
import {ApiBaseService} from './apiBase.service';
import {Http} from '@angular/http';
import {Router} from '@angular/router';
import {ReplaySubject} from 'rxjs/ReplaySubject';

@Injectable()
export class AuthenticationService extends ApiBaseService {

	private _lock: any;
	private _lockOptions = {
		auth: {
			nonce: '',
			redirectUrl: window.location.origin,
			responseType: 'id_token token',
		},
		autoclose: true,
		languageDictionary: {
			emailInputPlaceholder: 'arcanonn@canonn.science',
			title: 'Canonn Science',
		},
		oidcConformant: true,
		socialButtonStyle: <'big' | 'small'>('small'), // huh? Strange workaround around typings in TypeScript .oO
		theme: {
			logo: '/assets/canonn.png',
		},
	};
	private _showOptions = {
		auth: {
			params: {
				audience: '',
				scope: 'openid',
				state: '',
			},
		},
	};

	public userInformation: UserInformation = void 0;
	private userInformation$: Observable<UserInformation> = void 0;

	private clientConfigurationEmitter = new ReplaySubject<ClientConfiguration>(1);
	public get clientConfiguration$(): Observable<ClientConfiguration> {
		return this.clientConfigurationEmitter;
	}

	constructor(http: Http, authHttp: AuthHttp, private _router: Router) {
		super(http, authHttp);

		this.getClientConfiguration()
			.subscribe(
				(config) => {
					console.log('Client confg:' + JSON.stringify(config));
					this.createLockInstance(config);
				},
				(error) => console.log(error)
			);
	}

	public login() {
		this._showOptions.auth.params.state = this._router.url;

		console.log('Showing lock with options: ');
		console.log(JSON.stringify(this._showOptions));

		this._lock.show(this._showOptions);

		return false;
	}

	public signout() {
		this.logout();

		localStorage.removeItem('nonce_for_auth0');
		this._lock = void 0;

		this.clientConfiguration$
			.subscribe(c => this.createLockInstance(c),
				e => console.log(e)
			);

		return false;
	};

	private logout() {
		this.userInformation = void 0;

		localStorage.removeItem('id_token');
		localStorage.removeItem('access_token');
	}

	public isReady() {
		return (this._lock);
	}

	public hasPermission(permission: string): boolean {
		if (this.authenticated() && this.userInformation) {
			return (this.userInformation.permissions.includes(permission));
		}
		return false;
	}

	public authenticated() {
		try {
			const result = tokenNotExpired('access_token');
			if (result && !this.userInformation) {
				this.fetchUserInformation();
			}

			if (!result) {
				this.logout();
			}

			return result;
		} catch (e) {
			this.logout();
		}
	}

	private fetchUserInformation() {
		if (this.userInformation) {
			return;
		}

		if (!this.userInformation$) {
			this.clientConfiguration$.subscribe(ci => {
				const url = `https://${ci.domain}/tokeninfo`;

				const payload = {
					id_token: localStorage.getItem('id_token'),
				};

				if (!this.userInformation$) {

					this.userInformation$ = this._http.post(url, payload)
						.map(res => <UserInformation>(res.json()))
						.do(() => this.redirectIfRequired());

					this.userInformation$.subscribe(
						userInfo => {
							this.userInformation = userInfo;

							// when user has no info, these are undefined, so make sure we have some data
							this.userInformation.groups = this.userInformation.groups || [];
							this.userInformation.roles = this.userInformation.roles || [];
							this.userInformation.permissions = this.userInformation.permissions || [];

							this.userInformation$ = void 0;
						},
					);

				}
			});
		}
	}

	private getClientConfiguration(): Observable<ClientConfiguration> {
		const url = `${this._apiBaseUrl}/v1/clientconfiguration`;

		return this._http.get(url)
			.map((res) => {
				const obj = <ClientConfiguration>res.json();
				this._showOptions.auth.params.audience = obj.audience;

				this.clientConfigurationEmitter.next(obj);
				return (obj);
			})
			.retryWhen(err => err
				.delayWhen(val => Observable.timer(15000)) // retry after 15 seconds
			);
	}

	private createLockInstance(config: ClientConfiguration) {
		console.log('Initializing new lock instance with options:');
		this._lockOptions.auth.nonce = this.getNonce();
		console.log(JSON.stringify(this._lockOptions));
		const lock = new Auth0Lock(config.clientId, config.domain, this._lockOptions);

		lock.on('authenticated', (authResult) => {
			console.log('AUTHENTICATED AUTHENTICATED AUTHENTICATED AUTHENTICATED AUTHENTICATED AUTHENTICATED'
				+ 'AUTHENTICATED AUTHENTICATED AUTHENTICATED AUTHENTICATED AUTHENTICATED');
			console.log('AuthResult:' + JSON.stringify(authResult));
			localStorage.setItem('id_token', authResult.idToken);
			localStorage.setItem('access_token', authResult.accessToken);
			if (authResult.state) {
				localStorage.setItem('redirectUrl', decodeURIComponent(authResult.state));
			}

			console.log('ID: ' + authResult.idToken);
			console.log('ACCESS: ' + authResult.accessToken);

			this.fetchUserInformation();
		});

		lock.on('authorization_error', (error) => {
			console.log('Authorization ERROR: ' + error.errorDescription);

			lock.show({
				flashMessage: {
					type: 'error',
					text: error.errorDescription,
				}
			});
		});

		this._lock = lock;
	}

	private redirectIfRequired() {
		const redirectUrl = localStorage.getItem('redirectUrl');
		if (redirectUrl) {
			localStorage.removeItem('redirectUrl');
			this._router.navigate([redirectUrl]);
		}
	}

	private getNonce(): string {
		const storedNonce = localStorage.getItem('nonce_for_auth0');
		if (storedNonce) {
			return storedNonce;
		}

		const bytes = new Uint8Array(16);
		const random: any = window.crypto.getRandomValues(bytes);
		const result = [];
		const charset = '0123456789ABCDEFGHIJKLMNOPQRSTUVXYZabcdefghijklmnopqrstuvwxyz-._~';

		random.forEach(c => {
			result.push(charset[c % charset.length]);
		});

		const newNonce = result.join('');
		localStorage.setItem('nonce_for_auth0', newNonce);
		return newNonce;
	}

}
