import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from '../../../services/api/authentication.service';
import {BaseDataComponent} from '../baseData.component';
import {RuinBaseDataLookupService} from '../../../services/ruinBaseDataLookupService';
import {ObeliskModel} from '../../../models/obeliskModel';
import {ObeliskApiService} from '../../../services/api/obeliskApi.service';
import {CodexDataModel} from '../../../models/codexDataModel';
import {CodexDataApiService} from '../../../services/api/codexDataApi.service';
import {CodexBaseDataLookupService} from '../../../services/codexBaseDataLookupService';
import {ObeliskGroupModel} from '../../../models/obeliskGroupModel';

@Component({
	selector: 'app-obelisks',
	templateUrl: './obelisk.component.html',
	styleUrls: ['./obelisk.component.less'],
})
export class ObeliskComponent extends BaseDataComponent<ObeliskModel> implements OnInit {

	public obeliskGroups: ObeliskGroupModel[] = [];

	public codexData: CodexDataModel[] = [];
	public codexDataLookup: {
		[key: number]: CodexDataModel,
	} = {};

	constructor(
		api: ObeliskApiService,
		auth: AuthenticationService,
		public ruinBaseData: RuinBaseDataLookupService,
		public codexBaseData: CodexBaseDataLookupService,
		private codexDataApi: CodexDataApiService
	) {
		super(api, auth);
	}

	ngOnInit() {
		super.ngOnInit();

		this.codexDataApi.getAll()
			.subscribe((res) => {
				this.codexData = res;
				this.codexDataLookup = {};
				this.codexData.forEach((item) => this.codexDataLookup[item.id] = item);
			});
	}

	public ruintypeSelected() {
		this.obeliskGroups = this.ruinBaseData.obeliskGroupData.filter(og => og.ruintypeId === this.editingData.ruintypeId);
	}

	public discard() {
		super.discard();

		this.obeliskGroups = [];
	}

	public edit(data: ObeliskModel) {
		super.edit(data);

		this.editingData.ruintypeId = this.ruinBaseData.obeliskGroupData.find(og => og.id === this.editingData.obeliskgroupId).ruintypeId;
		this.ruintypeSelected();
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
