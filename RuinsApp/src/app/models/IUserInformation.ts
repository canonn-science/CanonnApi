import {IIdentity} from './IIdentity';

export interface IUserInformation {
	email: string;
	name: string;
	given_name: string;
	family_name: string;
	picture: string;
	gender: string;
	locale: string;
	nickname: string;
	groups: string[];
	roles: string[];
	permissions: string[];
	email_verified: boolean;
	updated_at: string;
	identities: IIdentity[];
	created_at: string;
}
