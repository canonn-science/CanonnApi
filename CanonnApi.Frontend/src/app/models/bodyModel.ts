import {SystemModel} from './systemModel';

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

	constructor(id?: number) {
		this.id = id;
	}
}
