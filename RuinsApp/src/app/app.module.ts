import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {Http, HttpModule, RequestOptions} from '@angular/http';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {AuthHttp, AuthConfig} from 'angular2-jwt';
import {Logger, Options as LoggerOptions} from 'angular2-logger/app/core/logger';
import {environment} from '../environments/environment';
import {LoginComponent} from './components/login/login.component';
import {MainSidebarComponent} from './components/main-sidebar/main-sidebar.component';
import {RelictsComponent} from './components/baseData/relicts.component';
import {RelictApiService} from './services/api/relictApi.service';
import {CodexCategoryComponent} from './components/baseData/codexCategory.component';
import {CodexDataComponent} from './components/baseData/codexData.component';
import {CodexDataApiService} from './services/api/codexDataApi.service';
import {AuthenticationService} from './services/api/authentication.service';
import {CodexCategoryApiService} from './services/api/codexCategoryApi.service';
import {BaseDataLookupService} from './services/baseDataLookupService';
import {AppHeaderComponent} from './components/app-header/app-header.component';
import {AlertModule} from 'ng2-bootstrap';
import {IntroComponent} from './components/intro/intro.component';
import {RuinTypeApiService} from './services/api/ruinTypeApi.service';
import {RuinTypeComponent} from 'app/components/baseData/ruintype.component';
import {ObeliskGroupComponent} from 'app/components/baseData/obeliskGroup.component';
import {ObeliskGroupApiService} from 'app/services/api/obeliskGroupApi.service';

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
		MainSidebarComponent,
		RelictsComponent,
		CodexCategoryComponent,
		CodexDataComponent,
		AppHeaderComponent,
		IntroComponent,
		RuinTypeComponent,
		ObeliskGroupComponent,
	],
	imports: [
		BrowserModule,
		FormsModule,
		HttpModule,
		AppRoutingModule,
		AlertModule.forRoot(),
	],
	providers: [
		{
			provide: AuthHttp,
			useFactory: authHttpServiceFactory,
			deps: [Http, RequestOptions]
		},
		{provide: LoggerOptions, useValue: {level: environment.initialLogLevel}},
		Logger,
		AuthenticationService,
		// Apis
		BaseDataLookupService,
		RelictApiService,
		CodexDataApiService,
		CodexCategoryApiService,
		RuinTypeApiService,
		ObeliskGroupApiService,
	],
	bootstrap: [AppComponent]
})
export class AppModule {
}
