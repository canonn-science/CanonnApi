import {BodyModel} from './bodyModel';
import {RuinTypeModel} from './ruintypeModel';
import {ObeliskGroupModel} from './obeliskGroupModel';
import {ObeliskModel} from './obeliskModel';

export class RuinSiteModel {
	public id: number;

	public bodyId: number;
	public ruintypeId: number;
	public latitude: number;
	public longitude: number;
	public created: string;
	public updated: string;

	public body: BodyModel;
	public ruintype: RuinTypeModel;

	// used for site editor exclusively
	public obeliskGroups: ObeliskGroupModel[];
	public obelisks: ObeliskModel[];
	public selectedSystem: string;
	public selectedBody: string;
	// site editor specifics end

	constructor(id?: number) {
		this.id = id;
	}
}
