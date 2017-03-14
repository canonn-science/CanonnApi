import {Component} from '@angular/core';
import {AuthenticationService} from '../../services/api/authentication.service';
import {BaseDataComponent} from 'app/components/baseData/baseData.component';
import {RuinlayoutVariantModel} from '../../models/ruinlayoutVariantModel';
import {RuinlayoutVariantApiService} from '../../services/api/ruinlayoutVariantApi.service';
import {BaseDataLookupService} from '../../services/baseDataLookupService';

@Component({
	selector: 'app-layoutvariants',
	templateUrl: './ruinlayoutVariant.component.html',
	styleUrls: ['./ruinlayoutVariant.component.less'],
})
export class RuinlayoutVariantComponent extends BaseDataComponent<RuinlayoutVariantModel> {

	constructor(api: RuinlayoutVariantApiService, auth: AuthenticationService, base: BaseDataLookupService) {
		super(api, auth, base);
	}

	public getNewDto() {
		return new RuinlayoutVariantModel(0);
	}

	public delete(entry: RuinlayoutVariantModel) {
		if (entry && window.confirm(`Really delete ruin layout variant ${entry.id} - ${entry.name}?`)) {
			this.api.delete(entry.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}
}
