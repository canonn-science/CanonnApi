import {CodexCategoryModel} from './codexCategory';

export class CodexDataModel {
	public id: number;
	public categoryId: number;
	public entryNumber: number;
	public text: string;
	public category: CodexCategoryModel;
	public created: string;
	public updated: string;
}
