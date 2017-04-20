import {SystemModel} from './systemModel';
import {RuinSiteModel} from './ruinSiteModel';

export class BodyModel {
	public id: number;
	public name: string;
	public systemId: number;
	public distance: number;
	public edsmExtId: number;
	public eddbExtId: number;
	public created: string;
	public updated: string;

	public system: SystemModel;
	public ruinSite: RuinSiteModel[];

	constructor(id?: number) {
		this.id = id;
	}
}
