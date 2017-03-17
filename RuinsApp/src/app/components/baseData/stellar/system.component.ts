import {Component} from '@angular/core';
import {AuthenticationService} from '../../../services/api/authentication.service';
import {BaseDataComponent} from '../baseData.component';
import {SystemModel} from '../../../models/systemModel';
import {SystemApiService} from '../../../services/api/systemApi.service';

@Component({
	selector: 'app-stellar-systems',
	templateUrl: './system.component.html',
	styleUrls: ['./system.component.less'],
})
export class SystemComponent extends BaseDataComponent<SystemModel> {

	constructor(api: SystemApiService, auth: AuthenticationService) {
		super(api, auth);
	}

	public getNewDto() {
		return new SystemModel(0);
	}

	public delete(entry: SystemModel) {
		if (entry && window.confirm(`Really delete system ${entry.id} - ${entry.name}?`)) {
			this.api.delete(entry.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}
}
