import {CodexCategoryModel} from './codexCategoryModel';

export class CodexDataModel {
	public id: number;
	public categoryId: number;
	public entryNumber: number;
	public text: string;
	public created: string;
	public updated: string;

	public category: CodexCategoryModel;

	constructor(id?: number) {
		this.id = id;
	}
}
