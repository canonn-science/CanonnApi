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
import {ArtifactComponent} from './components/baseData/codex/artifact.component';
import {ArtifactApiService} from './services/api/artifactApi.service';
import {CodexCategoryComponent} from './components/baseData/codex/codexCategory.component';
import {CodexDataComponent} from './components/baseData/codex/codexData.component';
import {CodexDataApiService} from './services/api/codexDataApi.service';
import {AuthenticationService} from './services/api/authentication.service';
import {CodexCategoryApiService} from './services/api/codexCategoryApi.service';
import {CodexBaseDataLookupService} from './services/codexBaseDataLookupService';
import {AppHeaderComponent} from './components/app-header/app-header.component';
import {AlertModule} from 'ng2-bootstrap';
import {IntroComponent} from './components/intro/intro.component';
import {RuinTypeApiService} from './services/api/ruinTypeApi.service';
import {RuinTypeComponent} from './components/baseData/ruins/ruintype.component';
import {ObeliskGroupComponent} from './components/baseData/ruins/obeliskGroup.component';
import {ObeliskGroupApiService} from 'app/services/api/obeliskGroupApi.service';
import {SystemApiService} from './services/api/systemApi.service';
import {BodyApiService} from './services/api/bodyApi.service';
import {RuinBaseDataLookupService} from './services/ruinBaseDataLookupService';
import {StellarBaseDataLookupService} from './services/stellarBaseDataLookupService';
import {SystemComponent} from './components/baseData/stellar/system.component';
import {BodyComponent} from './components/baseData/stellar/body.component';

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
		AppHeaderComponent,
		MainSidebarComponent,
		LoginComponent,
		IntroComponent,
		// Base data components
		ArtifactComponent,
		CodexCategoryComponent,
		CodexDataComponent,
		ObeliskGroupComponent,
		RuinTypeComponent,
		SystemComponent,
		BodyComponent,
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
		// Base data Lookups
		CodexBaseDataLookupService,
		RuinBaseDataLookupService,
		StellarBaseDataLookupService,
		// Apis
		ArtifactApiService,
		CodexCategoryApiService,
		CodexDataApiService,
		ObeliskGroupApiService,
		RuinTypeApiService,
		SystemApiService,
		BodyApiService,
	],
	bootstrap: [AppComponent]
})
export class AppModule {
}
