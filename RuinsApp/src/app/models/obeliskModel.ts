import {ArtifactModel} from './artifactModel';
import {CodexDataModel} from './codexDataModel';
import {ObeliskGroupModel} from './obeliskGroupModel';

export class ObeliskModel {
	public id: number;

	public obeliskgroupId: number;
	public number: number;
	public isBroken: boolean;
	public artifactId: number;
	public codexdataId: number;

	public created: string;
	public updated: string;

	public obeliskgroup: ObeliskGroupModel;
	public artifact: ArtifactModel;
	public codexdata: CodexDataModel;

	constructor(id?: number) {
		this.id = id;
	}
}