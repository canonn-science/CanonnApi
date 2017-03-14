import {RuinTypeModel} from './ruintypeModel';
import {ObeliskGroupModel} from './obeliskGroupModel';

export class RuinLayoutModel {
	public id: number;

	public name: string;
	public ruintypeId: number;
	public created: string;
	public updated: string;

	public ruintype: RuinTypeModel;
	public ObeliskGroups: ObeliskGroupModel[];

	constructor(id?: number) {
		this.id = id;
	}
}
