import {CodexDataModel} from './codexDataModel';
import {ArtifactModel} from './artifactModel';

export class CodexCategoryModel {
	public id: number;
	public name: string;
	public artifactId: number;
	public created: string;
	public updated: string;

	public codexData: CodexDataModel[];
	public artifact: ArtifactModel;

	constructor(id?: number) {
		this.id = id;
	}
}
