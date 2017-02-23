import {Injectable} from '@angular/core';
import { tokenNotExpired } from 'angular2-jwt';

@Injectable()
export class AuthService {

	private _lock = new Auth0Lock('dummy', 'canonn-science.eu.auth0.com', {});

	constructor() {
		this._lock.on('authenticated', (authResult) => {
			localStorage.setItem('id_token', authResult.idToken);
			localStorage.setItem('access_token', authResult.accessToken);
		});
	}

	public login() {
		this._lock.show();
	}

	public authenticated() {
		return tokenNotExpired();
	}

	public logout() {
		localStorage.removeItem('id_token');
		localStorage.removeItem('access_token');
	}
}
