import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from '../../services/api/authentication.service';
const {version: appVersion} = require('../../../../package.json');

@Component({
	selector: 'app-about',
	templateUrl: './about.component.html',
	styleUrls: ['./about.component.less']
})
export class AboutComponent implements OnInit {

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
