import {Component} from '@angular/core';
import {AuthenticationService} from '../../services/api/authentication.service';
import {BaseDataComponent} from 'app/components/baseData/baseData.component';
import {RuinLayoutApiService} from '../../services/api/ruinLayoutApi.service';
import {RuinLayoutModel} from '../../models/ruinLayoutModel';
import {BaseDataLookupService} from '../../services/baseDataLookupService';

@Component({
	selector: 'app-ruinlayouts',
	templateUrl: './ruinLayout.component.html',
	styleUrls: ['./ruinLayout.component.less'],
})
export class RuinLayoutComponent extends BaseDataComponent<RuinLayoutModel> {

	constructor(api: RuinLayoutApiService, auth: AuthenticationService, base: BaseDataLookupService) {
		super(api, auth, base);
	}

	public getNewDto() {
		return new RuinLayoutModel(0);
	}

	public delete(entry: RuinLayoutModel) {
		if (entry && window.confirm(`Really delete ruin layout ${entry.id} - ${entry.name}?`)) {
			this.api.delete(entry.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}
}
