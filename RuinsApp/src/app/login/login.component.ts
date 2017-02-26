import {Component} from '@angular/core';
import {AuthenticationService} from '../services/authentication.service';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.less'],
	providers: [AuthenticationService]
})
export class LoginComponent {

	constructor(public auth: AuthenticationService) {
	}

}
