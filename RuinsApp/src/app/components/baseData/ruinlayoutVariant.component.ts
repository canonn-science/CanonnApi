import {Component} from '@angular/core';
import {AuthenticationService} from '../../services/api/authentication.service';
import {BaseDataComponent} from 'app/components/baseData/baseData.component';
import {RuinlayoutVariantModel} from '../../models/ruinlayoutVariantModel';
import {LayoutVariantApiService} from '../../services/api/layoutVariantApi.service';

@Component({
	selector: 'app-layoutvariants',
	templateUrl: './ruinlayoutVariant.component.html',
	styleUrls: ['./ruinlayoutVariant.component.less'],
})
export class RuinlayoutVariantComponent extends BaseDataComponent<RuinlayoutVariantModel> {

	constructor(api: LayoutVariantApiService, auth: AuthenticationService) {
		super(api, auth, null);
	}

	public getNewDto() {
		return new RuinlayoutVariantModel(0);
	}

	public delete(entry: RuinlayoutVariantModel) {
		if (entry && window.confirm(`Really delete layout variant ${entry.id} - ${entry.name}?`)) {
			this.api.delete(entry.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}
}
