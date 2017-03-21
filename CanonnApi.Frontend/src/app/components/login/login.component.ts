import {Component} from '@angular/core';
import {AuthenticationService} from '../../services/api/authentication.service';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.less'],
})
export class LoginComponent {

	constructor(public auth: AuthenticationService) {
	}

}
