import {Component} from '@angular/core';
import {AuthenticationService} from '../services/authentication.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.less'],
	providers: [AuthenticationService],
})
export class AppComponent {
	title = 'RuinsApp - wip!';

	constructor(public auth: AuthenticationService) {
	}
}
