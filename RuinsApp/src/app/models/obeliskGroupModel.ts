import {RuinTypeModel} from './ruintypeModel';

export class ObeliskGroupModel {
	public id: number;
	public typeId: string;
	public name: string;
	public count: number;
	public created: string;
	public updated: string;

	/*
	public obelisks: ObeliskModel[];
	public ruinLayoutObeliskGroups: RuinLayoutObeliskGroupModel[];
	*/
	public ruinType: RuinTypeModel;

	constructor(id?: number) {
		this.id = id;
	}
}
