import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from '../../services/api/authentication.service';
const {version: appVersion} = require('../../../../package.json');
import {environment} from '../../../environments/environment';

@Component({
	selector: 'app-intro',
	templateUrl: './intro.component.html',
	styleUrls: ['./intro.component.less']
})
export class IntroComponent implements OnInit {

	public apiBaseUrl: string;
	public apiVersion: string;
	public version: string;

	constructor(private authenticationService: AuthenticationService) {
		this.apiBaseUrl = environment.apiBaseUri;
		this.apiVersion = 'not connected';
		this.version = appVersion;
	}

	ngOnInit() {
		this.authenticationService.clientConfiguration$.subscribe(
			(cc) => { this.apiVersion = cc.apiVersion; }
		);
	}

}
