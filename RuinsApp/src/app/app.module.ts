import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {Http, HttpModule, RequestOptions} from '@angular/http';
import {AppRoutingModule} from './app-routing.module';

import {AppComponent} from './app.component';
import {provideAuth, AuthHttp, AuthConfig} from 'angular2-jwt';

// currently angular2-jwt AUTH_PROVIDERS don't work, so use this workaround:
// https://github.com/auth0/angular2-jwt/issues/258
export function authHttpServiceFactory(http: Http, options: RequestOptions) {
	return new AuthHttp( new AuthConfig({}), http, options);
}

@NgModule({
	declarations: [
		AppComponent
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
			deps: [ Http, RequestOptions ]
		},
	],
	bootstrap: [AppComponent]
})
export class AppModule {
}
