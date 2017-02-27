import {CodexCategoryModel} from './codexCategory';

export class RelictsModel {
	public id: number;
	public name: string;
	public created: string;
	public updated: string;
	public codexCategory: CodexCategoryModel[];
}
