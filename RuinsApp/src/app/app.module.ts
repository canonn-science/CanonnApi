import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {Http, HttpModule, RequestOptions} from '@angular/http';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {AuthHttp, AuthConfig} from 'angular2-jwt';
import {Logger, Options as LoggerOptions} from 'angular2-logger/app/core/logger';
import {environment} from '../environments/environment';
import {LoginComponent} from './login/login.component';
import {MainmenuComponent} from './mainmenu/mainmenu.component';
import {RelictsComponent} from './relicts/relicts.component';
import {RelictsApiService} from './services/api/relictsApi.service';
import {CodexCategoryComponent} from './codex/codexCategory.component';
import {CodexDataComponent} from './codex/codexData.component';
import {CodexApiService} from './services/api/codexApi.service';
import {AuthenticationService} from './services/api/authentication.service';
import {LinqService} from 'ng2-linq';

// currently angular2-jwt AUTH_PROVIDERS don't work, so use this workaround:
// https://github.com/auth0/angular2-jwt/issues/258
export function authHttpServiceFactory(http: Http, options: RequestOptions) {
	return new AuthHttp(new AuthConfig({
		tokenName: 'id_token',
	}), http, options);
}

@NgModule({
	declarations: [
		AppComponent,
		LoginComponent,
		MainmenuComponent,
		RelictsComponent,
		CodexCategoryComponent,
		CodexDataComponent,
	],
	imports: [
		BrowserModule,
		FormsModule,
		HttpModule,
		AppRoutingModule,
	],
	providers: [
		{
			provide: AuthHttp,
			useFactory: authHttpServiceFactory,
			deps: [Http, RequestOptions]
		},
		{provide: LoggerOptions, useValue: {level: environment.initialLogLevel}},
		Logger,
		RelictsApiService,
		CodexApiService,
		AuthenticationService,
		LinqService,
	],
	bootstrap: [AppComponent]
})
export class AppModule {
}
