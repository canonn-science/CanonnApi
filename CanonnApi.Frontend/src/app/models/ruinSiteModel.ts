import {BodyModel} from './bodyModel';
import {RuinTypeModel} from './ruintypeModel';

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

	constructor(id?: number) {
		this.id = id;
	}
}
