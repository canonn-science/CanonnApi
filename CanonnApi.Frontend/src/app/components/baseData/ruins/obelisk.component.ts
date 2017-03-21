import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from '../../../services/api/authentication.service';
import {BaseDataComponent} from '../baseData.component';
import {RuinBaseDataLookupService} from '../../../services/ruinBaseDataLookupService';
import {ObeliskModel} from '../../../models/obeliskModel';
import {ObeliskApiService} from '../../../services/api/obeliskApi.service';
import {CodexDataModel} from '../../../models/codexDataModel';
import {CodexDataApiService} from '../../../services/api/codexDataApi.service';
import {CodexBaseDataLookupService} from '../../../services/codexBaseDataLookupService';

@Component({
	selector: 'app-obelisks',
	templateUrl: './obelisk.component.html',
	styleUrls: ['./obelisk.component.less'],
})
export class ObeliskComponent extends BaseDataComponent<ObeliskModel> implements OnInit {

	public codexData: CodexDataModel[] = [];
	public codexDataLookup: {
		[key: number]: CodexDataModel,
	} = {};

	constructor(
		api: ObeliskApiService, auth: AuthenticationService,
		public ruinBaseData: RuinBaseDataLookupService,
		public codexBaseData: CodexBaseDataLookupService,
		private codexDataApi: CodexDataApiService
	) {
		super(api, auth);

		this.codexDataApi.getAll()
			.subscribe((res) => {
				this.codexData = res;
				const temp = {};
				this.codexData.forEach((item) => temp[item.id] = item);
				this.codexDataLookup = temp;
			});
	}

	ngOnInit() {
		super.ngOnInit();
	}

	public getNewDto() {
		return new ObeliskModel(0);
	}

	public delete(item: ObeliskModel) {
		const message = `Really delete obelisk  ${item.id}?`;
		if (item && window.confirm(message)) {
			this.api.delete(item.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}
}
