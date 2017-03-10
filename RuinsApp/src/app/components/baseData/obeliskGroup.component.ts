import {Component} from '@angular/core';
import {AuthenticationService} from '../../services/api/authentication.service';
import {BaseDataComponent} from 'app/components/baseData/baseData.component';
import {ObeliskGroupModel} from '../../models/obeliskGroupModel';
import {BaseDataLookupService} from '../../services/baseDataLookupService';
import {ObeliskGroupApiService} from '../../services/api/obeliskGroupApi.service';

@Component({
	selector: 'app-obeliskgroups',
	templateUrl: './obeliskGroup.component.html',
	styleUrls: ['./obeliskGroup.component.less'],
})
export class ObeliskGroupComponent extends BaseDataComponent<ObeliskGroupModel> {

	constructor(api: ObeliskGroupApiService, auth: AuthenticationService, baseDataLookup: BaseDataLookupService) {
		super(api, auth, baseDataLookup);
	}

	public getNewDto() {
		return new ObeliskGroupModel(0);
	}

	public delete(item: ObeliskGroupModel) {
		const message = `Really delete obelisk group ${item.id} - ${this.baseData.ruinTypeLookup[item.typeId].name} ${item.name}?`;
		if (item && window.confirm(message)) {
			this.api.delete(item.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}
}
