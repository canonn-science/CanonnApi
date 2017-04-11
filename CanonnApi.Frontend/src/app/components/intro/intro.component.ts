import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from '../../services/api/authentication.service';
const {version: appVersion} = require('../../../../package.json');

@Component({
	selector: 'app-intro',
	templateUrl: './intro.component.html',
	styleUrls: ['./intro.component.less']
})
export class IntroComponent implements OnInit {

	public apiVersion: string;
	public version: string;

	constructor(private authenticationService: AuthenticationService) {
		this.apiVersion = 'not connected';
		this.version = appVersion;
	}

	ngOnInit() {
		this.authenticationService.clientConfiguration$.subscribe(
			(cc) => { this.apiVersion = cc.apiVersion; }
		);
	}

}
