import {RuinTypeModel} from './ruintypeModel';

export class ObeliskGroupModel {
	public id: number;
	public ruintypeId: string;
	public name: string;
	public count: number;
	public created: string;
	public updated: string;

	/*
	public obelisks: ObeliskModel[];
	public ruinLayoutObeliskGroups: RuinLayoutObeliskGroupModel[];
	*/
	public ruintype: RuinTypeModel;

	constructor(id?: number) {
		this.id = id;
	}
}
