import {Component} from '@angular/core';
import {RelictApiService} from '../../services/api/relictApi.service';
import {RelictModel} from '../../models/relictModel';
import {AuthenticationService} from '../../services/api/authentication.service';
import {BaseDataComponent} from 'app/components/baseData/baseData.component';

@Component({
	selector: 'app-relicts',
	templateUrl: './relicts.component.html',
	styleUrls: ['./relicts.component.less'],
})
export class RelictsComponent extends BaseDataComponent<RelictModel> {

	constructor(api: RelictApiService, auth: AuthenticationService) {
		super(api, auth, null);
	}

	public getNewDto() {
		return new RelictModel(0);
	}

	public delete(relict: RelictModel) {
		if (relict && window.confirm(`Really delete relict ${relict.id} - ${relict.name}?`)) {
			this.api.delete(relict.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}
}
