import {Component} from '@angular/core';
import {AuthService} from '../services/auth.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.less'],
	providers: [AuthService],
})
export class AppComponent {
	title = 'app works!';

	constructor(public auth: AuthService) {
	}
}
