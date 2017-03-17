import {Component} from '@angular/core';
import {AuthenticationService} from '../../../services/api/authentication.service';
import {BaseDataComponent} from '../baseData.component';
import {BodyModel} from '../../../models/bodyModel';
import {BodyApiService} from '../../../services/api/bodyApi.service';
import {StellarBaseDataLookupService} from '../../../services/stellarBaseDataLookupService';

@Component({
	selector: 'app-stellar-bodies',
	templateUrl: './body.component.html',
	styleUrls: ['./body.component.less'],
})
export class BodyComponent extends BaseDataComponent<BodyModel> {

	constructor(api: BodyApiService, auth: AuthenticationService, public baseData: StellarBaseDataLookupService) {
		super(api, auth);
	}

	public getNewDto() {
		return new BodyModel(0);
	}

	public delete(entry: BodyModel) {
		if (entry && window.confirm(`Really delete body ${entry.id} - ${this.baseData.systemLookup[entry.systemId].name} - ${entry.name}?`)) {
			this.api.delete(entry.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}

	public get sortedSystemData() {
		return this.baseData.systemData.sort((a, b) => (a.name !== b.name) ? a.name < b.name ? -1 : 1 : 0);
	}
}
