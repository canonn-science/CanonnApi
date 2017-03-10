// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

import {Level as LoggerLevel} from 'angular2-logger/app/core/level';

export const environment = {
	version: '1.0.0-beta1',
	production: false,
	apiBaseUri: 'http://localhost:52685',
	initialLogLevel: LoggerLevel.LOG,
};
