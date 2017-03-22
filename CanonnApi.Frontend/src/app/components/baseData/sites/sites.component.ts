import {Component, OnInit} from '@angular/core';
import {SystemApiService} from '../../../services/api/systemApi.service';
import {SystemModel} from '../../../models/systemModel';
import {BodyModel} from '../../../models/bodyModel';
import {BodyApiService} from '../../../services/api/bodyApi.service';
import {TypeaheadMatch} from 'ng2-bootstrap';
import {ObeliskModel} from '../../../models/obeliskModel';
import {ObeliskGroupApiService} from '../../../services/api/obeliskGroupApi.service';
import {ObeliskGroupModel} from '../../../models/obeliskGroupModel';
import {ObeliskApiService} from '../../../services/api/obeliskApi.service';
import {RuinBaseDataLookupService} from '../../../services/ruinBaseDataLookupService';

@Component({
	selector: 'app-sites',
	templateUrl: './sites.component.html',
	styleUrls: ['./sites.component.less']
})
export class SitesComponent implements OnInit {
	public systems: SystemModel[];
	public bodies: BodyModel[];
	public filteredBodies: BodyModel[];
	public selectedBody: BodyModel;
	public selectedSystem: SystemModel;
	public obeliskGroups: ObeliskGroupModel[];
	public obelisks: ObeliskModel[];
	public ruinTypeId: number;
	public availableObeliskGroups: ObeliskGroupModel[];

	constructor(private _systemApi: SystemApiService,
					private _bodyApi: BodyApiService,
					private _obeliskGroupApi: ObeliskGroupApiService,
					private _obeliskApi: ObeliskApiService,
					private _ruinData: RuinBaseDataLookupService) {
	}

	public ngOnInit() {
		this._systemApi.getAll()
			.subscribe(res => this.systems = res);
		this._bodyApi.getAll()
			.subscribe(res => this.bodies = res);
		this._obeliskGroupApi.getAll()
			.subscribe(res => this.obeliskGroups = res);
	}

	public systemSelect(match: TypeaheadMatch) {
		this.selectedBody = void 0;

		if (!!match.item.id) {
			this.filteredBodies = this.bodies.filter(obj => obj.systemId === match.item.id);
		}
	}

	public ruinTypeSelect() {
		this.availableObeliskGroups = this.obeliskGroups.filter(og => og.ruintypeId === this.ruinTypeId);
		this._obeliskApi.search(this.ruinTypeId)
			.subscribe(res => this.obelisks = res);
	}

	public toggleObelisk(obelisk: ObeliskModel) {
		obelisk.active = !obelisk.active;
	}

	public toggleObeliskGroup(obeliskGroup: ObeliskGroupModel) {
		obeliskGroup.active = !obeliskGroup.active;
	}

	public obelisksByGroupId(obeliskGroupId: number): ObeliskModel[] {
		return this.obelisks.filter(o => o.obeliskgroupId === obeliskGroupId);
	}
}
