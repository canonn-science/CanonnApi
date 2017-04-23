import {Component, OnInit} from '@angular/core';
import {BodyModel} from '../../../models/bodyModel';
import {ObeliskModel} from '../../../models/obeliskModel';
import {ObeliskGroupModel} from '../../../models/obeliskGroupModel';
import {ObeliskApiService} from '../../../services/api/obeliskApi.service';
import {RuinBaseDataLookupService} from '../../../services/ruinBaseDataLookupService';
import {StellarBaseDataLookupService} from '../../../services/stellarBaseDataLookupService';
import {RuinSiteModel} from '../../../models/ruinSiteModel';
import {RuinSitesApiService} from '../../../services/api/ruinSitesApi.service';
import {AuthenticationService} from '../../../services/api/authentication.service';

@Component({
	selector: 'app-sites',
	templateUrl: './sites.component.html',
	styleUrls: ['./sites.component.less']
})
export class SitesComponent implements OnInit {
	public data: RuinSiteModel[];
	public editingData: RuinSiteModel;
	public filteredBodies: BodyModel[];
	public selectedSystemId: number = void 0;

	constructor(
		public auth: AuthenticationService,
		public ruinSitesApiService: RuinSitesApiService,
		public stellarBaseData: StellarBaseDataLookupService,
		public ruinBaseData: RuinBaseDataLookupService,
		private _obeliskApi: ObeliskApiService) {
	}

	public ngOnInit() {
		this.loadData();
	}

	public loadData() {
		this.ruinSitesApiService.getAll()
			.subscribe(res => this.data = res);
	}

	public edit(item: RuinSiteModel) {
		this.ruinSitesApiService.getForEditor(item.id)
			.subscribe(res => {
				this.editingData = res;
			});

		this.selectedSystemId = this.stellarBaseData.bodyLookup[item.bodyId].systemId;
	}

	public createNew() {
		this.editingData = new RuinSiteModel(0);
	}

	public save() {
		const data = this.editingData;
		this.editingData = void 0;
		this.selectedSystemId = void 0;

		this.ruinSitesApiService.saveSite(data)
			.do(() => this.loadData())
			.subscribe();
	}

	public discard() {
		this.editingData = void 0;
		this.selectedSystemId = void 0;
	}

	public ruintypeSelected() {
		this.editingData.obeliskGroups = this.ruinBaseData.obeliskGroupData.filter(og => og.ruintypeId === this.editingData.ruintypeId);

		this._obeliskApi.search(this.editingData.ruintypeId)
			.subscribe(res => this.editingData.obelisks = res);
	}

	public toggleObeliskGroup(obeliskGroup: ObeliskGroupModel) {
		obeliskGroup.active = !obeliskGroup.active;
		if (!obeliskGroup.active) {
			this.obelisksByGroupId(obeliskGroup.id).forEach(o => o.active = false);
		}
	}

	public delete(item: RuinSiteModel) {
		if (item && window.confirm(`Really delete ruin site GS${item.id}?`)) {
			this.ruinSitesApiService.delete(item.id)
				.do(() => this.loadData())
				.subscribe();
		}
	}

	public toggleObelisk(obelisk: ObeliskModel) {
		obelisk.active = !obelisk.active;
	}

	public get sortedSystemData() {
		return this.stellarBaseData.systemData.sort((a, b) => (a.name !== b.name) ? a.name < b.name ? -1 : 1 : 0);
	}

	public bodiesBySystemId(): BodyModel[] {
		return this.stellarBaseData.bodyData.filter(obj => obj.systemId === this.selectedSystemId);
	}

	public obelisksByGroupId(obeliskGroupId: number): ObeliskModel[] {
		return this.editingData.obelisks.filter(o => o.obeliskgroupId === obeliskGroupId);
	}
}
