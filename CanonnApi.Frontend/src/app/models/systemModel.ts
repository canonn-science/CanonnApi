import {BodyModel} from './bodyModel';

export class SystemModel {
	public id: number;
	public name: string;
	public edsmExtId: number;
	public eddbExtId: number;
	public created: string;
	public updated: string;

	public bodies: BodyModel[];

	constructor(id?: number) {
		this.id = id;
	}
}
