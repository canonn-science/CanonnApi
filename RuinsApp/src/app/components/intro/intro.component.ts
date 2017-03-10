import {Component, OnInit} from '@angular/core';
import {environment} from '../../../environments/environment';

@Component({
	selector: 'app-intro',
	templateUrl: './intro.component.html',
	styleUrls: ['./intro.component.less']
})
export class IntroComponent implements OnInit {

	public version: string;

	constructor() {
		this.version = environment.version;
	}

	ngOnInit() {
	}

}
