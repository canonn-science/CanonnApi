import {RuinLayoutModel} from './ruinLayoutModel';

export class RuinlayoutVariantModel {
	public id: number;

	public name: string;
	public ruinlayoutId: number;
	public created: string;
	public updated: string;

	public ruinlayout: RuinLayoutModel;

	constructor(id?: number) {
		this.id = id;
	}
}
