import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from '../../services/api/authentication.service';
const {version: appVersion} = require('../../../../package.json');

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.less'],
})
export class LoginComponent implements OnInit {

	public apiVersion: string;
	public version: string;

	constructor(public auth: AuthenticationService) {
		this.apiVersion = 'not connected';
		this.version = appVersion;
	}

	ngOnInit() {
		this.auth.clientConfiguration$.subscribe(
			(cc) => { this.apiVersion = cc.apiVersion; }
		);
	}
}
