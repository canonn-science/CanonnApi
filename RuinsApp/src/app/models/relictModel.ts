import {CodexCategoryModel} from './codexCategoryModel';

export class RelictModel {
	public id: number;
	public name: string;
	public created: string;
	public updated: string;
	public codexCategory: CodexCategoryModel[];

	constructor(id?: number) {
		this.id = id;
	}
}
