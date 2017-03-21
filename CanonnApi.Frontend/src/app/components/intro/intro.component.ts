import {Component, OnInit} from '@angular/core';
const {version: appVersion} = require('../../../../package.json');

@Component({
	selector: 'app-intro',
	templateUrl: './intro.component.html',
	styleUrls: ['./intro.component.less']
})
export class IntroComponent implements OnInit {

	public version: string;

	constructor() {
		this.version = appVersion;
	}

	ngOnInit() {
	}

}
