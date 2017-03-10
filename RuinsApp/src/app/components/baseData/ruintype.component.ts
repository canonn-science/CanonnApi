import {Component} from '@angular/core';
import {AuthenticationService} from '../../services/api/authentication.service';
import {BaseDataComponent} from 'app/components/baseData/baseData.component';
import {RuinTypeModel} from '../../models/ruintypeModel';
import {RuinTypeApiService} from '../../services/api/ruinTypeApi.service';

@Component({
	selector: 'app-ruintypes',
	templateUrl: './ruintype.component.html',
	styleUrls: ['./ruintype.component.less'],
})
export class RuinTypeComponent extends BaseDataComponent<RuinTypeModel> {

	constructor(api: RuinTypeApiService, auth: AuthenticationService) {
		super(api, auth, null);
	}

	public getNewDto() {
		return new RuinTypeModel(0);
	}

	public delete(item: RuinTypeModel) {
		if (item && window.confirm(`Really delete ruin type ${item.id} - ${item.name}?`)) {
			this.api.delete(item.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}
}
