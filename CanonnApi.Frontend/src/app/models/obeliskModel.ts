import {ArtifactModel} from './artifactModel';
import {CodexDataModel} from './codexDataModel';
import {ObeliskGroupModel} from './obeliskGroupModel';

export class ObeliskModel {
	public id: number;

	public obeliskgroupId: number;
	public number: number;
	public isBroken: boolean;
	public isVerified: boolean;
	public artifactId: number;
	public codexdataId: number;
	public active: boolean;

	public created: string;
	public updated: string;

	public obeliskgroup: ObeliskGroupModel;
	public artifact: ArtifactModel;
	public codexdata: CodexDataModel;

	public ruintypeId: number; // just for editor

	constructor(id?: number) {
		this.id = id;
	}
}
