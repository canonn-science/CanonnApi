import {CodexDataModel} from './codexDataModel';

export class CodexCategoryModel {
	public id: number;
	public name: string;
	public primaryRelict: number;
	public created: string;
	public updated: string;
	public codexData: CodexDataModel[];

	constructor(id?: number) {
		this.id = id;
	}
}
