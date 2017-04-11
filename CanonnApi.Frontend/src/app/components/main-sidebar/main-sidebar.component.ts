import {Component} from '@angular/core';
import {Router} from '@angular/router';

@Component({
	selector: 'app-main-sidebar',
	templateUrl: './main-sidebar.component.html',
	styleUrls: ['./main-sidebar.component.less']
})
export class MainSidebarComponent {

	constructor(
		public router: Router) {
	}
}
