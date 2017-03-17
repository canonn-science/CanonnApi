import {Component} from '@angular/core';
import {AuthenticationService} from '../../../services/api/authentication.service';
import {BaseDataComponent} from '../baseData.component';
import {ObeliskGroupModel} from '../../../models/obeliskGroupModel';
import {ObeliskGroupApiService} from '../../../services/api/obeliskGroupApi.service';
import {RuinBaseDataLookupService} from '../../../services/ruinBaseDataLookupService';

@Component({
	selector: 'app-obeliskgroups',
	templateUrl: './obeliskGroup.component.html',
	styleUrls: ['./obeliskGroup.component.less'],
})
export class ObeliskGroupComponent extends BaseDataComponent<ObeliskGroupModel> {

	constructor(api: ObeliskGroupApiService, auth: AuthenticationService, public baseData: RuinBaseDataLookupService) {
		super(api, auth);
	}

	public getNewDto() {
		return new ObeliskGroupModel(0);
	}

	public delete(item: ObeliskGroupModel) {
		const message = `Really delete obelisk group ${item.id} - ${this.baseData.ruinTypeLookup[item.ruintypeId].name} ${item.name}?`;
		if (item && window.confirm(message)) {
			this.api.delete(item.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}
}
